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

public partial class adm_edit_post : System.Web.UI.Page
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

    private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
    DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
    
    public void filldetails()
    {
        try
        {
            SqlConnection con1 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
        DataTable dt = new DataTable();
        con1.Open();
        SqlDataReader myReader = null;
        SqlCommand myCommand = new SqlCommand("select * from job_site_posts where sr=@sr", con1);
        myCommand.Parameters.AddWithValue("@sr", Request.QueryString["id"].ToString());
        myReader = myCommand.ExecuteReader();

        while (myReader.Read())
        {
            page_short_url.Text = (myReader["page_short_url"].ToString());
            pin_code.Text = (myReader["pincode"].ToString());
            main_category.Text = (myReader["main_category"].ToString());
            sub_category.Text = (myReader["sub_category"].ToString());
            sch_industry.Text = (myReader["sch_industry"].ToString());
            sch_post_name.Text = (myReader["sch_post_name"].ToString());
            sch_qualification.Text = (myReader["sch_qualification"].ToString());
            sch_valid_through.Text = (myReader["sch_valid_through"].ToString());
            pdf_url.Text = (myReader["pdf_url"].ToString());
            sch_apply_now_url.Text = (myReader["sch_apply_now_url"].ToString());
            sch_responsibilities.Text = (myReader["sch_responsibilities"].ToString());
            sch_salery.Text = (myReader["sch_salery"].ToString());
            thumbnail_url.Text = (myReader["thumbnail_url"].ToString());
            schema_status.Text = (myReader["schema_status"].ToString());
            post_status.Text = (myReader["post_status"].ToString());
            last_update.Text = ("Last Update: " + myReader["last_update"].ToString());
            post_published.Text = ("Post Published: " + myReader["post_published"].ToString());
            sch_number_of_posts.Text = (myReader["sch_number_of_posts"].ToString());
            tags.Text = (myReader["tags"].ToString());

            state.Text = (myReader["state"].ToString());
            district.Text = (myReader["district"].ToString());
            area.Text = (myReader["area"].ToString());

            //excelState = (myReader["state"].ToString());
            //excelDistrict = (myReader["district"].ToString());
            //excelArea = (myReader["area"].ToString());

            state_hf.Value = (myReader["state"].ToString());
            district_hf.Value = (myReader["district"].ToString());
            area_hf.Value = (myReader["area"].ToString());
            updatesitemap_lbl.Text = myReader["post_status"].ToString();
            post_status_hf.Value = myReader["post_status"].ToString();

            statetxt.Text = (myReader["state"].ToString());
            districttxt.Text = (myReader["district"].ToString());
            areatxt.Text = (myReader["area"].ToString());

            try
            {
                submitted_date_lbl.Text = "Submitted Date:" + myReader["submitted_date"].ToString();
                submitted_by_lbl.Text = "Submitted By: <strong style='color:black; font-weight:bold;'>" + DecryptString(myReader["submitted_username"].ToString(), EncryptionKey) + "</strong>";
                updata_post.Text = "Approve Job";
            }
            catch (Exception ex)
            {
                submitted_date_lbl.Text = "";
                submitted_by_lbl.Text = "";
                updata_post.Text = "Update Post";
            }
        }
        con1.Close();
        con1.Dispose();
        myReader.Close();
    }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            filldetails();
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
                //Response.Write(ex.Message);
            }


            //load district
            try
            {
                pin_code.Text = "";
                area.Items.Clear();
                SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                con.Open();
                SqlCommand com = new SqlCommand("select DISTINCT state,district from tj_locations where state=@state order by district", con); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                com.Parameters.AddWithValue("@state", state_hf.Value.ToString());
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                district.DataTextField = ds.Tables[0].Columns["district"].ToString(); // text field name of table dispalyed in dropdown
                district.DataSource = ds.Tables[0]; //assigning datasource to the dropdownlist
                district.DataBind(); //binding dropdownlist
                district.Items.Insert(0, "Select District..");
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
            }
            ////load areas
            try
            {
                SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                con.Open();
                SqlCommand com = new SqlCommand("select DISTINCT state,district,area from tj_locations where district=@district order by area", con); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                com.Parameters.AddWithValue("@district", district_hf.Value.ToString());
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
            {
                //Response.Write(ex.Message);
            }

            filldetails();

            //fill thumbnail url if state is not empty and thumbnail is empty
            if (state.SelectedIndex != 0 && thumbnail_url.Text.Trim() == "")
            {
                try
                {
                    SqlConnection con1 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                    DataTable dt = new DataTable();
                    con1.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand("select * from job_site_images where state=@state and job_type=@job_type order by newid()", con1);
                    myCommand.Parameters.AddWithValue("@state", state.Text.ToString());
                    myCommand.Parameters.AddWithValue("@job_type", main_category.Text.ToString());
                    myReader = myCommand.ExecuteReader();
                    if (myReader.HasRows)
                    {
                        while (myReader.Read())
                        {
                            thumbnail_url.Text = (myReader["logourl"].ToString().Replace("https://agovt", "https://www.agovt"));
                            thumbnail_images_preview.ImageUrl = myReader["logourl"].ToString().Replace("https://agovt", "https://www.agovt");
                        }
                    }
                    else
                    {
                    }
                    myReader.Close();
                    con1.Close();
                    con1.Dispose();
                }
                catch (Exception ex)
                {
                }
                //try
                //{
                //    state.Items.Insert(0, excelState);
                //    district.Items.Insert(0, excelDistrict);
                //    area.Items.Insert(0, excelArea);
                //}
                //catch(Exception ex)
                //{
                //    Response.Write(ex.Message);
                //}
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


    public void create_custom_link()
    {
        page_short_url.Enabled = true;
        string url_district = "";
        if (sch_industry.Text.Contains(district.Text) == true)
        {
            url_district = "";
        }
        else
        {
            url_district = "in " + district.Text.Trim();
        }
        string urlkey = sch_industry.Text.Trim() + " " + sch_post_name.Text.Trim() + " jobs " + url_district.ToString() + " " + DateTime.Now.ToString("dd");
        urlkey = urlkey.Replace("  ", " ");
        urlkey = urlkey.Replace("  ", " ");
        urlkey = urlkey.Replace("  ", " ");
        urlkey = urlkey.Replace("  ", " ");
        urlkey = urlkey.Replace("  ", " ");
        urlkey = urlkey.Replace("  ", " ");
        urlkey = urlkey.Replace(" ", "-");
        urlkey = urlkey.Replace("?", "");
        urlkey = urlkey.Replace(".", "");
        urlkey = urlkey.Replace("/", "");
        urlkey = urlkey.Replace("~", "");
        urlkey = urlkey.Replace("!", "");
        urlkey = urlkey.Replace("@", "");
        urlkey = urlkey.Replace("#", "");
        urlkey = urlkey.Replace("$", "");
        urlkey = urlkey.Replace("%", "");
        urlkey = urlkey.Replace("^", "");
        urlkey = urlkey.Replace("&", "");
        urlkey = urlkey.Replace("*", "");
        urlkey = urlkey.Replace("(", "");
        urlkey = urlkey.Replace(")", "");
        urlkey = urlkey.Replace("_", "");
        urlkey = urlkey.Replace("+", "");
        urlkey = urlkey.Replace("`", "");
        urlkey = urlkey.Replace(";", "");
        urlkey = urlkey.Replace(":", "");
        urlkey = urlkey.Replace("'", "");
        urlkey = urlkey.Replace("|", "");
        urlkey = urlkey.Replace("{", "");
        urlkey = urlkey.Replace("[", "");
        urlkey = urlkey.Replace("}", "");
        urlkey = urlkey.Replace("]", "");
        urlkey = urlkey.Replace("<", "");
        urlkey = urlkey.Replace(">", "");
        urlkey = urlkey.Replace("–", " ");

        urlkey = urlkey.Replace("–", " ");
        urlkey = urlkey.Replace(",", " ");
        urlkey = urlkey.Replace("  ", " ");
        urlkey = urlkey.Replace("  ", " ");
        urlkey = urlkey.Replace("  ", " ");
        urlkey = urlkey.Replace("  ", " ");
        urlkey = urlkey.Replace("  ", " ");
        urlkey = urlkey.Replace(" ", "-");

        urlkey = urlkey.Replace("--", "-");
        urlkey = urlkey.Replace("--", "-");
        urlkey = urlkey.Replace("--", "-");
        urlkey = urlkey.Replace("--", "-");
        urlkey = urlkey.Replace("--", "-");
        urlkey = urlkey.Replace("--", "-");
        urlkey = urlkey.ToLower();
        page_short_url.Text = urlkey.ToString();
    }
    protected void createcustomlink_Click(object sender, EventArgs e)
    {
        create_custom_link();
    }

    private void replaceString(String filename, String search, String replace)
    {
        StreamReader sr = new StreamReader(filename);
        String[] rows = Regex.Split(sr.ReadToEnd(), "\r\n");
        sr.Close();

        StreamWriter sw = new StreamWriter(filename);
        for (int i = 0; i < rows.Length; i++)
        {
            if (rows[i].Contains(search))
            {
                rows[i] = rows[i].Replace(search, replace);
            }
            sw.WriteLine(rows[i]);
        }
        sw.Close();
    }

    public void updatepostfun()
    {
        try
        {
            if (state.SelectedValue=="" || state.SelectedValue=="Select State..")
            {
                //statetxt.Text = districttxt.Text;
            }
            else
            {
                statetxt.Text=state.SelectedValue;
            }
            if(statetxt.Text.Trim()=="")
            {
                statetxt.Text = districttxt.Text;
            }


            if (district.SelectedValue == "" || state.SelectedValue == "Select District..")
            {
                //statetxt.Text = districttxt.Text;
            }
            else
            {
                districttxt.Text = district.SelectedValue;
            }
            if (districttxt.Text.Trim() == "")
            {
                districttxt.Text = statetxt.Text;
            }

            if (area.SelectedValue == "" || state.SelectedValue == "Select Area..")
            {
                //statetxt.Text = districttxt.Text;
            }
            else
            {
                areatxt.Text = area.SelectedValue;
            }
            if (areatxt.Text.Trim() == "")
            {
                areatxt.Text = areatxt.Text;
            }

            errorlbl.Visible = false;
            successlbl.Visible = false;

            if (page_short_url.Text.Trim().ToString() == "")
            {
                create_custom_link();
            }
            else
            {

            }
            string str_tags = tags.Text.Trim().ToString();
            str_tags = str_tags.Replace(",", ", ");
            str_tags = Regex.Replace(str_tags, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
            str_tags = str_tags.Replace(", ", ",");

            SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
            SqlCommand cmd = new SqlCommand("update job_site_posts Set sch_industry=@sch_industry,query_sch_industry=@query_sch_industry,sch_organization_name=@sch_organization_name,query_sch_organization_name=@query_sch_organization_name,sch_post_name=@sch_post_name,query_sch_post_name=@query_sch_post_name,state=@state,query_state=@query_state,district=@district,query_district=@query_district,area=@area,query_area=@query_area,pincode=@pincode,sch_number_of_posts=@sch_number_of_posts,main_category=@main_category,query_main_category=@query_main_category,sub_category=@sub_category,query_sub_category=@query_sub_category,sch_qualification=@sch_qualification,query_sch_qualification=@query_sch_qualification,sch_education_requirements=@sch_education_requirements,query_sch_education_requirements=@query_sch_education_requirements,sch_valid_through=@sch_valid_through,sch_last_date=@sch_last_date,pdf_url=@pdf_url,sch_apply_now_url=@sch_apply_now_url,sch_responsibilities=@sch_responsibilities,sch_skills=@sch_skills,sch_salery=@sch_salery,sch_base_salery=@sch_base_salery,sch_min_salery=@sch_min_salery,sch_max_salery=@sch_max_salery,sch_employment_type=@sch_employment_type,sch_working_hours=@sch_working_hours,sch_street_address=@sch_street_address,sch_locality=@sch_locality,sch_region=@sch_region,sch_pay_scale=@sch_pay_scale,sch_experience_requirements=@sch_experience_requirements,thumbnail_url=@thumbnail_url,tags=@tags,query_tags=@query_tags,page_short_url=@page_short_url,page_full_url=@page_full_url,post_status=@post_status,schema_status=@schema_status,post_published=@post_published,last_update=@last_update,query_valid_through=@query_valid_through where sr=@sr", con);
            cmd.Parameters.AddWithValue("@sr", Request.QueryString["id"].ToString());

            cmd.Parameters.AddWithValue("@sch_industry", sch_industry.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@query_sch_industry", sch_industry.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-"));
            cmd.Parameters.AddWithValue("@sch_organization_name", sch_industry.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@query_sch_organization_name", sch_industry.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-"));

            cmd.Parameters.AddWithValue("@sch_post_name", sch_post_name.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@query_sch_post_name", sch_post_name.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace(",", "-").Replace("--", "-").Replace("--", "-").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-").Replace("--", "-").Replace("--", "-"));
            cmd.Parameters.AddWithValue("@state", statetxt.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@query_state", statetxt.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-"));
            cmd.Parameters.AddWithValue("@district", districttxt.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@query_district", districttxt.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-"));
            cmd.Parameters.AddWithValue("@area", areatxt.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@query_area", areatxt.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-"));
            cmd.Parameters.AddWithValue("@pincode", pin_code.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@sch_number_of_posts", sch_number_of_posts.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@main_category", main_category.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@query_main_category", main_category.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-"));
            cmd.Parameters.AddWithValue("@sub_category", sub_category.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@query_sub_category", sub_category.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-"));

            cmd.Parameters.AddWithValue("@sch_qualification", sch_qualification.Text.Trim().ToString().Replace("/", ","));
            cmd.Parameters.AddWithValue("@query_sch_qualification", sch_qualification.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-").Replace(".", "-").Replace("--", "-").Replace("/", ","));
            cmd.Parameters.AddWithValue("@sch_education_requirements", sch_qualification.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@query_sch_education_requirements", sch_qualification.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-").Replace(".", "-").Replace("--", "-"));

            cmd.Parameters.AddWithValue("@sch_valid_through", sch_valid_through.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@sch_last_date", sch_valid_through.Text.Trim().ToString());

            cmd.Parameters.AddWithValue("@pdf_url", pdf_url.Text.Trim().ToString());

            cmd.Parameters.AddWithValue("@sch_apply_now_url", sch_apply_now_url.Text.Trim().ToString());

            cmd.Parameters.AddWithValue("@sch_responsibilities", sch_responsibilities.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@sch_skills", sch_responsibilities.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@sch_salery", sch_salery.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@sch_base_salery", sch_salery.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@sch_min_salery", (Convert.ToInt32(sch_salery.Text.Trim()) - 5000)).ToString();
            cmd.Parameters.AddWithValue("@sch_max_salery", (Convert.ToInt32(sch_salery.Text.Trim()) + 5000)).ToString();
            cmd.Parameters.AddWithValue("@sch_employment_type", "FULL_TIME");
            cmd.Parameters.AddWithValue("@sch_working_hours", "9AM to 5PM");
            cmd.Parameters.AddWithValue("@sch_street_address", areatxt.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@sch_locality", districttxt.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@sch_region", statetxt.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@sch_pay_scale", "1200");
            cmd.Parameters.AddWithValue("@sch_experience_requirements", "Freshers and Experienced");
            cmd.Parameters.AddWithValue("@thumbnail_url", thumbnail_url.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@tags", str_tags);
            cmd.Parameters.AddWithValue("@query_tags", tags.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-"));
            cmd.Parameters.AddWithValue("@page_short_url", page_short_url.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@page_full_url", "https://job.freshgovtjobs.in/view-job/" + page_short_url.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@post_status", post_status.Text);
            cmd.Parameters.AddWithValue("@schema_status", schema_status.Text);
            cmd.Parameters.AddWithValue("@post_published", indianTime.ToString("yyyy-MM-ddTHH:mm:ss"));

            cmd.Parameters.AddWithValue("@last_update", indianTime.ToString("yyyy-MM-ddTHH:mm:ss"));
            cmd.Parameters.AddWithValue("@query_valid_through", sch_valid_through.Text.Trim().ToString().Replace("-", ""));

            con.Open();
            int count = cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            //filldetails();
            successlbl.Text = "Data Updated Sucessfully";
            successlbl.Visible = true;
            if(updata_post.Text=="Approve job")
            {
                updatesitemap();
            }
        }
        catch (Exception ex)
        {
            errorlbl.Text = ex.Message;
            errorlbl.Visible = true;
        }
    }

    public void updatesitemap()
    {
        try
        {
            //count total jobs
            int totalJobs = 0;
            int fileNumber = 0;
            try
            {
                SqlConnection con0 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string myScalarQuery0 = "select count(*) from job_site_posts where post_status = 'Published'";
                SqlCommand myCommand0 = new SqlCommand(myScalarQuery0, con0);
                myCommand0.Connection.Open();
                int countuser0 = (int)myCommand0.ExecuteScalar();
                con0.Close();
                con0.Dispose();
                totalJobs = countuser0;
                fileNumber = 0;
            }
            catch(Exception ex)
            { }

            if (totalJobs <= 1000)
            {
                fileNumber = 1;
            }
            else if (totalJobs > 1000 && totalJobs <= 2000)
            {
                fileNumber = 2;
            }
            else if (totalJobs > 2000 && totalJobs <= 3000)
            {
                fileNumber = 3;
            }
            else if (totalJobs > 3000 && totalJobs <= 4000)
            {
                fileNumber = 4;
            }
            else if (totalJobs > 4000 && totalJobs <= 5000)
            {
                fileNumber = 5;
            }
            else if (totalJobs > 5000 && totalJobs <= 6000)
            {
                fileNumber = 6;
            }
            else if (totalJobs > 6000 && totalJobs <= 7000)
            {
                fileNumber = 7;
            }
            else if (totalJobs > 7000 && totalJobs <= 8000)
            {
                fileNumber = 8;
            }
            else if (totalJobs > 8000 && totalJobs <= 9000)
            {
                fileNumber = 9;
            }
            else if (totalJobs > 9000 && totalJobs <= 10000)
            {
                fileNumber = 10;
            }
            else if (totalJobs > 10000 && totalJobs <= 11000)
            {
                fileNumber = 11;
            }
            else if (totalJobs > 11000 && totalJobs <= 12000)
            {
                fileNumber = 12;
            }
            else if (totalJobs > 12000 && totalJobs <= 13000)
            {
                fileNumber = 13;
            }
            else if (totalJobs > 13000 && totalJobs <= 14000)
            {
                fileNumber = 14;
            }
            else if (totalJobs > 14000 && totalJobs <= 15000)
            {
                fileNumber = 15;
            }
            else if (totalJobs > 15000 && totalJobs <= 16000)
            {
                fileNumber = 16;
            }
            else if (totalJobs > 16000 && totalJobs <= 17000)
            {
                fileNumber = 17;
            }
            else if (totalJobs > 17000 && totalJobs <= 18000)
            {
                fileNumber = 18;
            }
            else if (totalJobs > 18000 && totalJobs <= 19000)
            {
                fileNumber = 19;
            }
            else if (totalJobs > 19000 && totalJobs <= 20000)
            {
                fileNumber = 20;
            }
            else if (totalJobs > 20000 && totalJobs <= 21000)
            {
                fileNumber = 21;
            }
            else if (totalJobs > 21000 && totalJobs <= 22000)
            {
                fileNumber = 22;
            }
            else if (totalJobs > 22000 && totalJobs <= 23000)
            {
                fileNumber = 23;
            }
            else if (totalJobs > 23000 && totalJobs <= 24000)
            {
                fileNumber = 24;
            }
            else if (totalJobs > 24000 && totalJobs <= 25000)
            {
                fileNumber = 25;
            }
            else if (totalJobs > 25000 && totalJobs <= 26000)
            {
                fileNumber = 26;
            }
            else if (totalJobs > 26000 && totalJobs <= 27000)
            {
                fileNumber = 27;
            }
            else if (totalJobs > 27000 && totalJobs <= 28000)
            {
                fileNumber = 28;
            }
            else if (totalJobs > 28000 && totalJobs <= 29000)
            {
                fileNumber = 29;
            }
            else if (totalJobs > 29000 && totalJobs <= 30000)
            {
                fileNumber = 30;
            }
            else if (totalJobs > 30000 && totalJobs <= 31000)
            {
                fileNumber = 31;
            }
            else if (totalJobs > 31000 && totalJobs <= 32000)
            {
                fileNumber = 32;
            }
            else if (totalJobs > 32000 && totalJobs <= 33000)
            {
                fileNumber = 33;
            }
            else if (totalJobs > 33000 && totalJobs <= 34000)
            {
                fileNumber = 34;
            }
            else if (totalJobs > 34000 && totalJobs <= 35000)
            {
                fileNumber = 35;
            }
            else if (totalJobs > 35000 && totalJobs <= 36000)
            {
                fileNumber = 36;
            }
            else if (totalJobs > 36000 && totalJobs <= 37000)
            {
                fileNumber = 37;
            }
            else if (totalJobs > 37000 && totalJobs <= 38000)
            {
                fileNumber = 38;
            }
            else if (totalJobs > 38000 && totalJobs <= 39000)
            {
                fileNumber = 39;
            }
            else if (totalJobs > 39000 && totalJobs <= 40000)
            {
                fileNumber = 40;
            }
            else if (totalJobs > 40000 && totalJobs <= 41000)
            {
                fileNumber = 41;
            }
            else if (totalJobs > 41000 && totalJobs <= 42000)
            {
                fileNumber = 42;
            }
            else if (totalJobs > 42000 && totalJobs <= 43000)
            {
                fileNumber = 43;
            }
            else if (totalJobs > 43000 && totalJobs <= 44000)
            {
                fileNumber = 44;
            }
            else if (totalJobs > 44000 && totalJobs <= 45000)
            {
                fileNumber = 45;
            }
            else if (totalJobs > 45000 && totalJobs <= 46000)
            {
                fileNumber = 46;
            }
            else if (totalJobs > 46000 && totalJobs <= 47000)
            {
                fileNumber = 47;
            }
            else if (totalJobs > 47000 && totalJobs <= 48000)
            {
                fileNumber = 48;
            }
            else if (totalJobs > 48000 && totalJobs <= 49000)
            {
                fileNumber = 49;
            }
            else if (totalJobs > 49000 && totalJobs <= 50000)
            {
                fileNumber = 50;
            }
            else if (totalJobs > 50000 && totalJobs <= 51000)
            {
                fileNumber = 51;
            }
            else if (totalJobs > 51000 && totalJobs <= 52000)
            {
                fileNumber = 52;
            }
            else if (totalJobs > 52000 && totalJobs <= 53000)
            {
                fileNumber = 53;
            }
            else if (totalJobs > 53000 && totalJobs <= 54000)
            {
                fileNumber = 54;
            }
            else if (totalJobs > 54000 && totalJobs <= 55000)
            {
                fileNumber = 55;
            }
            else if (totalJobs > 55000 && totalJobs <= 56000)
            {
                fileNumber = 56;
            }
            else if (totalJobs > 56000 && totalJobs <= 57000)
            {
                fileNumber = 57;
            }
            else if (totalJobs > 57000 && totalJobs <= 58000)
            {
                fileNumber = 58;
            }
            else if (totalJobs > 58000 && totalJobs <= 59000)
            {
                fileNumber = 59;
            }
            else if (totalJobs > 59000 && totalJobs <= 60000)
            {
                fileNumber = 60;
            }
            else if (totalJobs > 60000 && totalJobs <= 61000)
            {
                fileNumber = 61;
            }
            else if (totalJobs > 61000 && totalJobs <= 62000)
            {
                fileNumber = 62;
            }
            else if (totalJobs > 62000 && totalJobs <= 63000)
            {
                fileNumber = 63;
            }
            else if (totalJobs > 63000 && totalJobs <= 64000)
            {
                fileNumber = 64;
            }
            else if (totalJobs > 64000 && totalJobs <= 65000)
            {
                fileNumber = 65;
            }
            else if (totalJobs > 65000 && totalJobs <= 66000)
            {
                fileNumber = 66;
            }
            else if (totalJobs > 66000 && totalJobs <= 67000)
            {
                fileNumber = 67;
            }
            else if (totalJobs > 67000 && totalJobs <= 68000)
            {
                fileNumber = 68;
            }
            else if (totalJobs > 68000 && totalJobs <= 69000)
            {
                fileNumber = 69;
            }
            else if (totalJobs > 69000 && totalJobs <= 70000)
            {
                fileNumber = 70;
            }
            else if (totalJobs > 70000 && totalJobs <= 71000)
            {
                fileNumber = 71;
            }
            else if (totalJobs > 71000 && totalJobs <= 72000)
            {
                fileNumber = 72;
            }
            else if (totalJobs > 72000 && totalJobs <= 73000)
            {
                fileNumber = 73;
            }
            else if (totalJobs > 73000 && totalJobs <= 74000)
            {
                fileNumber = 74;
            }
            else if (totalJobs > 74000 && totalJobs <= 75000)
            {
                fileNumber = 75;
            }
            else if (totalJobs > 75000 && totalJobs <= 76000)
            {
                fileNumber = 76;
            }
            else if (totalJobs > 76000 && totalJobs <= 77000)
            {
                fileNumber = 77;
            }
            else if (totalJobs > 77000 && totalJobs <= 78000)
            {
                fileNumber = 78;
            }
            else if (totalJobs > 78000 && totalJobs <= 79000)
            {
                fileNumber = 79;
            }
            else if (totalJobs > 79000 && totalJobs <= 80000)
            {
                fileNumber = 80;
            }
            else if (totalJobs > 80000 && totalJobs <= 81000)
            {
                fileNumber = 81;
            }
            else if (totalJobs > 81000 && totalJobs <= 82000)
            {
                fileNumber = 82;
            }
            else if (totalJobs > 82000 && totalJobs <= 83000)
            {
                fileNumber = 83;
            }
            else if (totalJobs > 83000 && totalJobs <= 84000)
            {
                fileNumber = 84;
            }
            else if (totalJobs > 84000 && totalJobs <= 85000)
            {
                fileNumber = 85;
            }
            else if (totalJobs > 85000 && totalJobs <= 86000)
            {
                fileNumber = 86;
            }
            else if (totalJobs > 86000 && totalJobs <= 87000)
            {
                fileNumber = 87;
            }
            else if (totalJobs > 87000 && totalJobs <= 88000)
            {
                fileNumber = 88;
            }
            else if (totalJobs > 88000 && totalJobs <= 89000)
            {
                fileNumber = 89;
            }
            else if (totalJobs > 89000 && totalJobs <= 90000)
            {
                fileNumber = 90;
            }
            else if (totalJobs > 90000 && totalJobs <= 91000)
            {
                fileNumber = 91;
            }
            else if (totalJobs > 91000 && totalJobs <= 92000)
            {
                fileNumber = 92;
            }
            else if (totalJobs > 92000 && totalJobs <= 93000)
            {
                fileNumber = 93;
            }
            else if (totalJobs > 93000 && totalJobs <= 94000)
            {
                fileNumber = 94;
            }
            else if (totalJobs > 94000 && totalJobs <= 95000)
            {
                fileNumber = 95;
            }
            else if (totalJobs > 95000 && totalJobs <= 96000)
            {
                fileNumber = 96;
            }
            else if (totalJobs > 96000 && totalJobs <= 97000)
            {
                fileNumber = 97;
            }
            else if (totalJobs > 97000 && totalJobs <= 98000)
            {
                fileNumber = 98;
            }
            else if (totalJobs > 98000 && totalJobs <= 99000)
            {
                fileNumber = 99;
            }
            else if (totalJobs > 99000 && totalJobs <= 100000)
            {
                fileNumber = 100;
            }
            else if (totalJobs > 100000 && totalJobs <= 101000)
            {
                fileNumber = 101;
            }
            else
            {
                fileNumber = 00;
            }
            string path = Server.MapPath("~/sitemap-job-" + fileNumber.ToString() + ".xml");

            string site_content = "";
            DataTable dt = new DataTable();
            int remainder = totalJobs - Convert.ToInt32(fileNumber.ToString() + "000");
            SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));

            //string strcon = "select * from job_site_posts where post_status = 'Published' order by sr DESC OFFSET " + fileNumber.ToString() + "000" + " ROWS FETCH NEXT " + remainder.ToString().Replace("-", "") + " ROWS ONLY;";
            string strcon = "select * from job_site_posts where post_status = 'Published' order by sr ASC OFFSET " + (fileNumber-1).ToString() + "000" + " ROWS FETCH NEXT " + remainder.ToString().Replace("-", "") + " ROWS ONLY;";
            SqlCommand cmd = new SqlCommand(strcon, con);
            //cmd.Parameters.AddWithValue("@sr", Request.QueryString["id"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "content");
            //site_content = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>\n";
            site_content = "<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">\n";
            int totalitemsinds = ds.Tables[0].Rows.Count;
            try
            {
                for (int n = 0; n < totalitemsinds; n++)
                {
                    DataRow drow = ds.Tables["content"].Rows[n];
                    string lastModDate = "";
                    if (drow.ItemArray.GetValue(45).ToString().Length == 10)
                    {
                        lastModDate = drow.ItemArray.GetValue(45).ToString() + "T08:17:52+00:00";
                    }
                    else if (drow.ItemArray.GetValue(45).ToString().Length == 19)
                    {
                        lastModDate = drow.ItemArray.GetValue(45).ToString() + "+00:00";
                    }
                    else
                    {
                        lastModDate = drow.ItemArray.GetValue(45).ToString();
                    }
                    site_content += "<url>\n";
                    site_content += "<loc>" + drow.ItemArray.GetValue(44) + "</loc>\n";
                    site_content += "<lastmod>" + lastModDate + "</lastmod>\n";
                    site_content += "<changefreq>hourly</changefreq>\n";
                    site_content += "<priority>0.8</priority>\n";
                    site_content += "</url>\n";
                }
            }
            catch (Exception ex)
            {
                successlbl.Text = ex.Message;
                successlbl.Visible = true;
                return;
            }
            con.Close();
            con.Dispose();
            site_content += "</urlset>\n";
            File.WriteAllText(path, String.Empty);
            File.AppendAllText(path, site_content);
            //job_errorlbl.Text = "Job's sitemap Created Successfully";
            //job_errorlbl.Visible = true;
            successlbl.Text = "Data Updated Sucessfully<br>"+ "<a href='https://job.freshgovtjobs.in/sitemap-job-" + fileNumber.ToString() + ".xml' target='_blank'>sitemap-job-"+ fileNumber.ToString()+"</a>";
            successlbl.Visible = true;
        }
        catch(Exception ex)
        {
            successlbl.Text = ex.Message;
            successlbl.Visible = true;
        }
    }

    protected void updata_post_Click(object sender, EventArgs e)
    {
        updatepostfun();
        updatesitemap();
    }


    protected void thumbimgauto_Click(object sender, EventArgs e)
    {

        try
        {
            SqlConnection con1 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
            DataTable dt = new DataTable();
            con1.Open();
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select * from job_site_images where state=@state order by newid()", con1);
            myCommand.Parameters.AddWithValue("@state", state.Text.ToString());
            myReader = myCommand.ExecuteReader();
            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    thumbnail_url.Text = (myReader["logourl"].ToString());
                    thumbnail_images_preview.ImageUrl = myReader["logourl"].ToString();
                }
            }
            else
            {
            }
            myReader.Close();
            con1.Close();
            con1.Dispose();
        }
        catch (Exception ex)
        {
        }
    }

}