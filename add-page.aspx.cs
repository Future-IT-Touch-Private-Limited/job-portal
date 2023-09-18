using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Data;
using System.Text;
using System.IO;

public partial class adm_add_page : System.Web.UI.Page
{
    string EncryptionKey = "ravindersinghgodara123admin";
    string EncryptionKey2 = "rvndr@123@adm";
    public static string MD5Hash(string text)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
        byte[] result = md5.Hash;
        StringBuilder strBuilder = new StringBuilder();
        for (int i = 0; i < result.Length; i++)
        {
            strBuilder.Append(result[i].ToString("x2"));
        }
        return strBuilder.ToString();
    }
    public string EncryptString(string Message, string Passphrase)
    {
        byte[] Results;
        System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
        MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
        byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));
        TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
        TDESAlgorithm.Key = TDESKey;
        TDESAlgorithm.Mode = CipherMode.ECB;
        TDESAlgorithm.Padding = PaddingMode.PKCS7;
        byte[] DataToEncrypt = UTF8.GetBytes(Message);
        try
        {
            ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
            Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
        }
        finally
        {
            TDESAlgorithm.Clear();
            HashProvider.Clear();
        }
        return Convert.ToBase64String(Results);
    }

    public string DecryptString(string Message, string Passphrase)
    {
        byte[] Results;
        System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
        MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
        byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));
        TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
        TDESAlgorithm.Key = TDESKey;
        TDESAlgorithm.Mode = CipherMode.ECB;
        TDESAlgorithm.Padding = PaddingMode.PKCS7;
        byte[] DataToDecrypt = Convert.FromBase64String(Message);
        try
        {
            ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
            Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
        }
        finally
        {
            TDESAlgorithm.Clear();
            HashProvider.Clear();
        }
        return UTF8.GetString(Results);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //load state
            try
            {
                area.Items.Clear();
                district.Items.Clear();
                SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                con.Open();
                SqlCommand com = new SqlCommand("select DISTINCT state from tj_locations order by state", con); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                //com.Parameters.AddWithValue("@statename", choosestateddl.SelectedValue.ToString());
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                state.DataTextField = ds.Tables[0].Columns["state"].ToString(); // text field name of table dispalyed in dropdown
                state.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                state.DataBind();  //binding dropdownlist
                state.Items.Insert(0, "Select State..");
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
            }
        }
    }


    protected void state_SelectedIndexChanged(object sender, EventArgs e)
    {
        //load district
        try
        {
            area.Items.Clear();
            SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
            con.Open();
            SqlCommand com = new SqlCommand("select DISTINCT state,district from tj_locations where state=@state order by district", con); // table name 
            SqlDataAdapter da = new SqlDataAdapter(com);
            com.Parameters.AddWithValue("@state", state.SelectedValue.ToString());
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset
            district.DataTextField = ds.Tables[0].Columns["district"].ToString(); // text field name of table dispalyed in dropdown
            district.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            district.DataBind();  //binding dropdownlist
            district.Items.Insert(0, "Select District..");
            con.Close();
            con.Dispose();
        }
        catch (Exception ex)
        {
        }
    }

    protected void district_SelectedIndexChanged(object sender, EventArgs e)
    {
        //load areas
        try
        {
            SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
            con.Open();
            SqlCommand com = new SqlCommand("select DISTINCT state,district,area from tj_locations where district=@district order by area", con); // table name 
            SqlDataAdapter da = new SqlDataAdapter(com);
            com.Parameters.AddWithValue("@district", district.SelectedValue.ToString());
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset
            area.DataTextField = ds.Tables[0].Columns["area"].ToString(); // text field name of table dispalyed in dropdown
            area.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            area.DataBind();  //binding dropdownlist
            area.Items.Insert(0, "Select Area..");
            con.Close();
            con.Dispose();
        }
        catch (Exception ex)
        { }
    }

    protected void area_SelectedIndexChanged(object sender, EventArgs e)
    {
        //load pin code
        try
        {
            DataTable dt7 = new DataTable();
            SqlConnection con7 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
            string strcon7 = "select * from tj_locations where state=@state and district=@district and area=@area";
            SqlCommand cmd7 = new SqlCommand(strcon7, con7);
            SqlDataAdapter da7 = new SqlDataAdapter(cmd7);
            cmd7.Parameters.AddWithValue("@state", state.Text.ToString());
            cmd7.Parameters.AddWithValue("@district", district.Text.ToString());
            cmd7.Parameters.AddWithValue("@area", area.Text.ToString());
            DataSet ds7 = new DataSet();
            da7.Fill(ds7, "content");
            DataRow drow7 = ds7.Tables["content"].Rows[0];
            pin_code.Text = drow7.ItemArray.GetValue(4).ToString();
            con7.Close();
            con7.Dispose();
        }
        catch (Exception ex)
        {
        }
    }



    protected void publish_page_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con2 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
            SqlCommand cmd2 = new SqlCommand("Insert into job_pages (meta_title,meta_description,meta_keywords,page_full_url,page_short_url,body,body2,state,district,area,pin_code,main_category,sub_category,published_date,published_day,published_month,published_year,post_status,post_type,main_category_query,sub_category_query,state_query,district_query,post_type_query,thumbnail_url,tags,industry,post_name,industry_query) values(@meta_title,@meta_description,@meta_keywords,@page_full_url,@page_short_url,@body,@body2,@state,@district,@area,@pin_code,@main_category,@sub_category,@published_date,@published_day,@published_month,@published_year,@post_status,@post_type,@main_category_query,@sub_category_query,@state_query,@district_query,@post_type_query,@thumbnail_url,@tags,@industry,@post_name,@industry_query)", con2);
            cmd2.Parameters.AddWithValue("@meta_title", meta_title.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@meta_description", meta_description.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@meta_keywords", meta_keywords.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@page_full_url", "https://agovtjobs.in/view-" + page_short_url.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@page_short_url", page_short_url.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@body", body.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@body2", body2.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@state", state.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@district", district.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@area", area.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@pin_code", pin_code.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@main_category", main_category.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@sub_category", sub_category.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@published_date", DateTime.Now.ToString("yyyy-MM-dd"));
            cmd2.Parameters.AddWithValue("@published_day", DateTime.Now.ToString("dd"));
            cmd2.Parameters.AddWithValue("@published_month", DateTime.Now.ToString("MM"));
            cmd2.Parameters.AddWithValue("@published_year", DateTime.Now.ToString("yyyy"));
            cmd2.Parameters.AddWithValue("@post_status", "Published");
            cmd2.Parameters.AddWithValue("@post_type", post_type.Text.Trim().ToString());

            string main_category_query = main_category.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-");
            string sub_category_query = main_category.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-");
            string state_query = state.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-");
            string district_query = district.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-");
            string post_type_query = post_type.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-");
            cmd2.Parameters.AddWithValue("@main_category_query", main_category_query.Trim());
            cmd2.Parameters.AddWithValue("@sub_category_query", sub_category_query.Trim());
            cmd2.Parameters.AddWithValue("@state_query", state_query.Trim());
            cmd2.Parameters.AddWithValue("@district_query", district_query.Trim());
            cmd2.Parameters.AddWithValue("@post_type_query", post_type_query.Trim());
            cmd2.Parameters.AddWithValue("@thumbnail_url", thumbnail_url.Text.Trim());
            cmd2.Parameters.AddWithValue("@tags", tags.Text.Trim());

            string str_industry = industry.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-");
            cmd2.Parameters.AddWithValue("@industry", industry.Text.Trim());
            cmd2.Parameters.AddWithValue("@post_name", post_name.Text.Trim());
            cmd2.Parameters.AddWithValue("@industry_query", str_industry);
            con2.Open();
            int count2 = cmd2.ExecuteNonQuery();
            con2.Close();
            con2.Dispose();
            Response.Redirect("all-pages.aspx");
        }
        catch (Exception ex)
        { }
    }

    protected void draft_page_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con2 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
            SqlCommand cmd2 = new SqlCommand("Insert into job_pages (meta_title,meta_description,meta_keywords,page_full_url,page_short_url,body,body2,state,district,area,pin_code,main_category,sub_category,published_date,published_day,published_month,published_year,post_status,post_type,main_category_query,sub_category_query,state_query,district_query,post_type_query,thumbnail_url,tags,industry,post_name,industry_query) values(@meta_title,@meta_description,@meta_keywords,@page_full_url,@page_short_url,@body,@body2,@state,@district,@area,@pin_code,@main_category,@sub_category,@published_date,@published_day,@published_month,@published_year,@post_status,@post_type,@main_category_query,@sub_category_query,@state_query,@district_query,@post_type_query,@thumbnail_url,@tags,@industry,@post_name,@industry_query)", con2);
            cmd2.Parameters.AddWithValue("@meta_title", meta_title.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@meta_description", meta_description.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@meta_keywords", meta_keywords.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@page_full_url", "https://agovtjobs.in/view-" + page_short_url.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@page_short_url", page_short_url.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@body", body.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@body2", body2.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@state", state.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@district", district.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@area", area.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@pin_code", pin_code.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@main_category", main_category.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@sub_category", sub_category.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@published_date", DateTime.Now.ToString("yyyy-MM-dd"));
            cmd2.Parameters.AddWithValue("@published_day", DateTime.Now.ToString("dd"));
            cmd2.Parameters.AddWithValue("@published_month", DateTime.Now.ToString("MM"));
            cmd2.Parameters.AddWithValue("@published_year", DateTime.Now.ToString("yyyy"));
            cmd2.Parameters.AddWithValue("@post_status", "Draft");
            cmd2.Parameters.AddWithValue("@post_type", post_type.Text.Trim().ToString());

            string main_category_query = main_category.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-");
            string sub_category_query = main_category.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-");
            string state_query = state.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-");
            string district_query = district.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-");
            string post_type_query = post_type.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-");

            cmd2.Parameters.AddWithValue("@main_category_query", main_category_query.Trim());
            cmd2.Parameters.AddWithValue("@sub_category_query", sub_category_query.Trim());
            cmd2.Parameters.AddWithValue("@state_query", state_query.Trim());
            cmd2.Parameters.AddWithValue("@district_query", district_query.Trim());
            cmd2.Parameters.AddWithValue("@post_type_query", post_type_query.Trim());
            cmd2.Parameters.AddWithValue("@thumbnail_url", thumbnail_url.Text.Trim());
            cmd2.Parameters.AddWithValue("@tags", tags.Text.Trim());

            string str_industry = industry.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-");

            cmd2.Parameters.AddWithValue("@industry", industry.Text.Trim());
            cmd2.Parameters.AddWithValue("@post_name", post_name.Text.Trim());
            cmd2.Parameters.AddWithValue("@industry_query", str_industry);
            con2.Open();
            int count2 = cmd2.ExecuteNonQuery();
            con2.Close();
            con2.Dispose();
            Response.Redirect("all-pages.aspx");
        }
        catch (Exception ex)
        { }
    }
}