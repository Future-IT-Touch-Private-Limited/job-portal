﻿using System;
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
using System.Globalization;

public partial class adm_add_extra_post : System.Web.UI.Page
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
                state.Items.Insert(0, "All India");
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
        try
        {
            if (thumbnail_url.Text.Trim() == "")
            {
                SqlConnection con1 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                DataTable dt = new DataTable();
                con1.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select top 1 * from job_site_images where state=@state order by newid()", con1);
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
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void create_custom_link()
    {
        page_short_url.Enabled = true;

        string urlkey = sch_industry.Text.Trim() + " " + sch_post_name.Text.Trim() + " "+post_type.Text.ToLower()+" " + DateTime.Now.ToString("dd");
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


    protected void publish_post_Click(object sender, EventArgs e)
    {
        try
        {
            errorlbl.Visible = false;
            if (main_category.Text.Trim() == "")
            {
                errorlbl.Text = "Invalid Main Category";
                errorlbl.Visible = true;
                return;
            }
            if (sub_category.Text.Trim() == "")
            {
                errorlbl.Text = "Invalid Sub Category";
                errorlbl.Visible = true;
                return;
            }
            if (sch_industry.Text.Trim() == "")
            {
                errorlbl.Text = "Invalid Industry Name";
                errorlbl.Visible = true;
                return;
            }
            if (sch_post_name.Text.Trim() == "")
            {
                errorlbl.Text = "Invalid Post Name";
                errorlbl.Visible = true;
                return;
            }
            if (sch_number_of_posts.Text.Trim() == "")
            {
                errorlbl.Text = "Invalid Total Posts";
                errorlbl.Visible = true;
                return;
            }
            if (sch_qualification.Text.Trim() == "")
            {
                errorlbl.Text = "Invalid Qualifications";
                errorlbl.Visible = true;
                return;
            }
            if (pdf_url.Text.Trim() == "")
            {
                errorlbl.Text = "Invalid PDF URL";
                errorlbl.Visible = true;
                return;
            }
            
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

            SqlConnection con2 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
            SqlCommand cmd2 = new SqlCommand("Insert into job_site_extra_posts (sch_industry,query_sch_industry,sch_post_name,query_sch_post_name,state,query_state,sch_number_of_posts,main_category,query_main_category,sub_category,query_sub_category,sch_qualification,query_sch_qualification,pdf_url,thumbnail_url,tags,query_tags,page_short_url,page_full_url,last_update,post_published,post_status,post_type) values(@sch_industry,@query_sch_industry,@sch_post_name,@query_sch_post_name,@state,@query_state,@sch_number_of_posts,@main_category,@query_main_category,@sub_category,@query_sub_category,@sch_qualification,@query_sch_qualification,@pdf_url,@thumbnail_url,@tags,@query_tags,@page_short_url,@page_full_url,@last_update,@post_published,@post_status,@post_type)", con2);

            cmd2.Parameters.AddWithValue("@sch_industry", sch_industry.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@query_sch_industry", sch_industry.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-"));

            cmd2.Parameters.AddWithValue("@sch_post_name", sch_post_name.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@query_sch_post_name", sch_post_name.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace(",", "-").Replace("--", "-").Replace("--", "-").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-").Replace("--", "-").Replace("--", "-"));
            cmd2.Parameters.AddWithValue("@state", state.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@query_state", state.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-"));
            cmd2.Parameters.AddWithValue("@sch_number_of_posts", sch_number_of_posts.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@main_category", main_category.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@query_main_category", main_category.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-"));
            cmd2.Parameters.AddWithValue("@sub_category", sub_category.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@query_sub_category", sub_category.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-").Replace("/", ","));

            cmd2.Parameters.AddWithValue("@sch_qualification", sch_qualification.Text.Trim().ToString().Replace("/", ","));
            cmd2.Parameters.AddWithValue("@query_sch_qualification", sch_qualification.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-").Replace(".", "-").Replace("--", "-"));

            cmd2.Parameters.AddWithValue("@pdf_url", pdf_url.Text.Trim().ToString());

            cmd2.Parameters.AddWithValue("@thumbnail_url", thumbnail_url.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@tags", str_tags);
            cmd2.Parameters.AddWithValue("@query_tags", tags.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-"));
            cmd2.Parameters.AddWithValue("@page_short_url", page_short_url.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@page_full_url", "https://agovtjobs.in/view-"+ post_type.Text.Trim().ToLower().Replace(" ","-").ToString()+ "/" + page_short_url.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@last_update", DateTime.Now.ToString("yyyy-MM-dd"));
            cmd2.Parameters.AddWithValue("@post_published", DateTime.Now.ToString("yyyy-MM-dd"));
            cmd2.Parameters.AddWithValue("@post_status", "Published");
            cmd2.Parameters.AddWithValue("@post_type", post_type.Text.Trim().ToString());
            con2.Open();
            int count2 = cmd2.ExecuteNonQuery();
            con2.Close();
            con2.Dispose();
            Response.Redirect("all-extra-posts.aspx");
        }
        catch (Exception ex)
        {
            errorlbl.Text = ex.Message;
            errorlbl.Visible = true;
        }
    }


    protected void draft_post_Click(object sender, EventArgs e)
    {
        try
        {
            errorlbl.Visible = false;
            if (main_category.Text.Trim() == "")
            {
                errorlbl.Text = "Invalid Main Category";
                errorlbl.Visible = true;
                return;
            }
            if (sub_category.Text.Trim() == "")
            {
                errorlbl.Text = "Invalid Sub Category";
                errorlbl.Visible = true;
                return;
            }
            if (sch_industry.Text.Trim() == "")
            {
                errorlbl.Text = "Invalid Industry Name";
                errorlbl.Visible = true;
                return;
            }
            if (sch_post_name.Text.Trim() == "")
            {
                errorlbl.Text = "Invalid Post Name";
                errorlbl.Visible = true;
                return;
            }
            if (sch_number_of_posts.Text.Trim() == "")
            {
                errorlbl.Text = "Invalid Total Posts";
                errorlbl.Visible = true;
                return;
            }
            if (sch_qualification.Text.Trim() == "")
            {
                errorlbl.Text = "Invalid Qualifications";
                errorlbl.Visible = true;
                return;
            }
            if (pdf_url.Text.Trim() == "")
            {
                errorlbl.Text = "Invalid PDF URL";
                errorlbl.Visible = true;
                return;
            }
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

            SqlConnection con2 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
            SqlCommand cmd2 = new SqlCommand("Insert into job_site_extra_posts (sch_industry,query_sch_industry,sch_post_name,query_sch_post_name,state,query_state,sch_number_of_posts,main_category,query_main_category,sub_category,query_sub_category,sch_qualification,query_sch_qualification,pdf_url,thumbnail_url,tags,query_tags,page_short_url,page_full_url,last_update,post_published,post_status,post_type) values(@sch_industry,@query_sch_industry,@sch_post_name,@query_sch_post_name,@state,@query_state,@sch_number_of_posts,@main_category,@query_main_category,@sub_category,@query_sub_category,@sch_qualification,@query_sch_qualification,@pdf_url,@thumbnail_url,@tags,@query_tags,@page_short_url,@page_full_url,@last_update,@post_published,@post_status,@post_type)", con2);

            cmd2.Parameters.AddWithValue("@sch_industry", sch_industry.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@query_sch_industry", sch_industry.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-"));

            cmd2.Parameters.AddWithValue("@sch_post_name", sch_post_name.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@query_sch_post_name", sch_post_name.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace(",", "-").Replace("--", "-").Replace("--", "-").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-").Replace("--", "-").Replace("--", "-"));
            cmd2.Parameters.AddWithValue("@state", state.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@query_state", state.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-"));
            cmd2.Parameters.AddWithValue("@sch_number_of_posts", sch_number_of_posts.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@main_category", main_category.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@query_main_category", main_category.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-"));
            cmd2.Parameters.AddWithValue("@sub_category", sub_category.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@query_sub_category", sub_category.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-").Replace("/", ","));

            cmd2.Parameters.AddWithValue("@sch_qualification", sch_qualification.Text.Trim().ToString().Replace("/", ","));
            cmd2.Parameters.AddWithValue("@query_sch_qualification", sch_qualification.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-").Replace(".", "-").Replace("--", "-"));

            cmd2.Parameters.AddWithValue("@pdf_url", pdf_url.Text.Trim().ToString());

            cmd2.Parameters.AddWithValue("@thumbnail_url", thumbnail_url.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@tags", str_tags);
            cmd2.Parameters.AddWithValue("@query_tags", tags.Text.Trim().ToLower().Replace("(", " ").Replace(")", " ").Replace("&", " ").Replace("#", " ").Replace("?", " ").Replace("_", " ").Replace("--", "-").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", "-"));
            cmd2.Parameters.AddWithValue("@page_short_url", page_short_url.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@page_full_url", "https://agovtjobs.in/view-" + post_type.Text.Trim().ToLower().Replace(" ", "-").ToString() + "/" + page_short_url.Text.Trim().ToString());
            cmd2.Parameters.AddWithValue("@last_update", DateTime.Now.ToString("yyyy-MM-dd"));
            cmd2.Parameters.AddWithValue("@post_published", DateTime.Now.ToString("yyyy-MM-dd"));
            cmd2.Parameters.AddWithValue("@post_status", "Draft");
            cmd2.Parameters.AddWithValue("@post_type", post_type.Text.Trim().ToString());
            con2.Open();
            int count2 = cmd2.ExecuteNonQuery();
            con2.Close();
            con2.Dispose();
            Response.Redirect("drafts-extra-posts.aspx");
        }
        catch (Exception ex)
        {
            errorlbl.Text = ex.Message;
            errorlbl.Visible = true;
        }
    }

    protected void thumbimgauto_Click(object sender, EventArgs e)
    {

        try
        {
            if (thumbnail_url.Text.Trim() == "")
            {
                SqlConnection con1 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                DataTable dt = new DataTable();
                con1.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select * from job_site_extra_posts where state=@state order by newid()", con1);
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
            }
        }
        catch (Exception ex)
        {
        }
    }
}