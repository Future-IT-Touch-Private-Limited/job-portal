using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adm_generate_sitemap : System.Web.UI.Page
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
        if(!IsPostBack)
        {
            //count total jobs
            try
            {
                SqlConnection con0 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string myScalarQuery0 = "select count(*) from job_site_posts where post_status = 'Published'";
                SqlCommand myCommand0 = new SqlCommand(myScalarQuery0, con0);
                myCommand0.Connection.Open();
                int countuser0 = (int)myCommand0.ExecuteScalar();
                con0.Close();
                con0.Dispose();
                job_numbers.Text = countuser0.ToString();
            }
            catch (Exception ex)
            {
                job_numbers.Text = "0";
            }

            //count admit_card_numbers
            try
            {
                SqlConnection con0 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string myScalarQuery0 = "select count(*) from job_site_admitcards where post_status = 'Published'";
                SqlCommand myCommand0 = new SqlCommand(myScalarQuery0, con0);
                myCommand0.Connection.Open();
                int countuser0 = (int)myCommand0.ExecuteScalar();
                con0.Close();
                con0.Dispose();
                admit_card_numbers.Text = countuser0.ToString();
            }
            catch (Exception ex)
            {
                admit_card_numbers.Text = "0";
            }

            //count answer_key_numbers
            try
            {
                SqlConnection con0 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string myScalarQuery0 = "select count(*) from job_site_extra_posts where post_status = 'Published' and post_type='Answer Keys'";
                SqlCommand myCommand0 = new SqlCommand(myScalarQuery0, con0);
                myCommand0.Connection.Open();
                int countuser0 = (int)myCommand0.ExecuteScalar();
                con0.Close();
                con0.Dispose();
                answer_key_numbers.Text = countuser0.ToString();
            }
            catch (Exception ex)
            {
                answer_key_numbers.Text = "0";
            }

            //count exam_date_numbers
            try
            {
                SqlConnection con0 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string myScalarQuery0 = "select count(*) from job_site_exam_interview where post_status = 'Published' and post_type='Exam Dates'";
                SqlCommand myCommand0 = new SqlCommand(myScalarQuery0, con0);
                myCommand0.Connection.Open();
                int countuser0 = (int)myCommand0.ExecuteScalar();
                con0.Close();
                con0.Dispose();
                exam_date_numbers.Text = countuser0.ToString();
            }
            catch (Exception ex)
            {
                exam_date_numbers.Text = "0";
            }

            //count interview_schedule_numbers
            try
            {
                SqlConnection con0 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string myScalarQuery0 = "select count(*) from job_site_exam_interview where post_status = 'Published' and post_type='Interview Scheduled'";
                SqlCommand myCommand0 = new SqlCommand(myScalarQuery0, con0);
                myCommand0.Connection.Open();
                int countuser0 = (int)myCommand0.ExecuteScalar();
                con0.Close();
                con0.Dispose();
                interview_schedule_numbers.Text = countuser0.ToString();
            }
            catch (Exception ex)
            {
                interview_schedule_numbers.Text = "0";
            }

            //count qualification_numbers
            try
            {
                SqlConnection con0 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string myScalarQuery0 = "select count(*) from job_site_qualification";
                SqlCommand myCommand0 = new SqlCommand(myScalarQuery0, con0);
                myCommand0.Connection.Open();
                int countuser0 = (int)myCommand0.ExecuteScalar();
                con0.Close();
                con0.Dispose();
                qualification_numbers.Text = countuser0.ToString();
            }
            catch (Exception ex)
            {
                qualification_numbers.Text = "0";
            }

            //count result_numbers
            try
            {
                SqlConnection con0 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string myScalarQuery0 = "select count(*) from job_site_results where post_status = 'Published'";
                SqlCommand myCommand0 = new SqlCommand(myScalarQuery0, con0);
                myCommand0.Connection.Open();
                int countuser0 = (int)myCommand0.ExecuteScalar();
                con0.Close();
                con0.Dispose();
                result_numbers.Text = countuser0.ToString();
            }
            catch (Exception ex)
            {
                result_numbers.Text = "0";
            }

            //count syllabus_numbers
            try
            {
                SqlConnection con0 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string myScalarQuery0 = "select count(*) from job_site_syllabus where post_status = 'Published'";
                SqlCommand myCommand0 = new SqlCommand(myScalarQuery0, con0);
                myCommand0.Connection.Open();
                int countuser0 = (int)myCommand0.ExecuteScalar();
                con0.Close();
                con0.Dispose();
                syllabus_numbers.Text = countuser0.ToString();
            }
            catch (Exception ex)
            {
                syllabus_numbers.Text = "0";
            }

            //count tag_numbers
            try
            {
                SqlConnection con0 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string myScalarQuery0 = "select count(*) from job_site_tag";
                SqlCommand myCommand0 = new SqlCommand(myScalarQuery0, con0);
                myCommand0.Connection.Open();
                int countuser0 = (int)myCommand0.ExecuteScalar();
                con0.Close();
                con0.Dispose();
                tag_numbers.Text = countuser0.ToString();
            }
            catch (Exception ex)
            {
                tag_numbers.Text = "0";
            }

            //count written_mark_numbers
            try
            {
                SqlConnection con0 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string myScalarQuery0 = "select count(*) from job_site_extra_posts where post_status = 'Published' and post_type='Written Marks'";
                SqlCommand myCommand0 = new SqlCommand(myScalarQuery0, con0);
                myCommand0.Connection.Open();
                int countuser0 = (int)myCommand0.ExecuteScalar();
                con0.Close();
                con0.Dispose();
                written_mark_numbers.Text = countuser0.ToString();
            }
            catch (Exception ex)
            {
                written_mark_numbers.Text = "0";
            }





        }
    }

    protected void generate_job_sitemap_Click(object sender, EventArgs e)
    {
        job_errorlbl.Visible = false;
        if(job_numbers.Text.Contains(".")==true)
        {
            job_errorlbl.Text = "Invalid total number of jobs";
            job_errorlbl.Visible = true;
            return;
        }
        if (job_numbers.Text.Contains("-") == true)
        {
            job_errorlbl.Text = "Invalid total number of jobs";
            job_errorlbl.Visible = true;
            return;
        }
        if (job_numbers.Text.Trim().Length<1)
        {
            job_errorlbl.Text = "Invalid total number of jobs";
            job_errorlbl.Visible = true;
            return;
        }
        try
        {
            string path = Server.MapPath("~/sitemap-job.xml");
            if (File.Exists(path))
            {
                string site_content = "";
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string strcon = "select top "+ job_numbers.Text.Trim().ToString()+ " * from job_site_posts where post_status = 'Published' order by sr DESC";
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
                        string lastModDate="";
                        if(drow.ItemArray.GetValue(45).ToString().Length==10)
                        {
                            lastModDate = drow.ItemArray.GetValue(45).ToString() + "T08:17:52+00:00";
                        }
                        else if(drow.ItemArray.GetValue(45).ToString().Length == 19)
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
                    job_errorlbl.Text = ex.Message;
                    job_errorlbl.Visible = true;
                }
                con.Close();
                con.Dispose();
                site_content += "</urlset>\n";
                File.WriteAllText(path, String.Empty);
                File.AppendAllText(path, site_content);
                job_errorlbl.Text = "Job's sitemap Created Successfully";
                job_errorlbl.Visible = true;
            }
            else
            {
                job_errorlbl.Text = "sitemap-job.xml File not found";
                job_errorlbl.Visible = true;
                return;
            }
        }
        catch (Exception ex)
        {
            job_errorlbl.Text = ex.Message;
            job_errorlbl.Visible = true;
            return;
        }
    }
    
    protected void admit_card_btn_Click(object sender, EventArgs e)
    {
        admit_card_errorlbl.Visible = false;
        if (admit_card_numbers.Text.Contains(".") == true)
        {
            admit_card_errorlbl.Text = "Invalid total number of admit cards";
            admit_card_errorlbl.Visible = true;
            return;
        }
        if (admit_card_numbers.Text.Contains("-") == true)
        {
            admit_card_errorlbl.Text = "Invalid total number of admit cards";
            admit_card_errorlbl.Visible = true;
            return;
        }
        if (admit_card_numbers.Text.Trim().Length < 1)
        {
            admit_card_errorlbl.Text = "Invalid total number of admit cards";
            admit_card_errorlbl.Visible = true;
            return;
        }
        try
        {
            string path = Server.MapPath("~/sitemap-admit-card.xml");
            if (File.Exists(path))
            {
                string site_content = "";
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string strcon = "select top " + admit_card_numbers.Text.Trim().ToString() + " * from job_site_admitcards where post_status = 'Published' order by sr DESC";
                SqlCommand cmd = new SqlCommand(strcon, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "content");
                site_content = "<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">\n";
                int totalitemsinds = ds.Tables[0].Rows.Count;
                try
                {
                    for (int n = 0; n < totalitemsinds; n++)
                    {
                        DataRow drow = ds.Tables["content"].Rows[n];
                        site_content += "<url>\n";
                        site_content += "<loc>" + drow.ItemArray.GetValue(20) + "</loc>\n";
                        site_content += "<lastmod>" + drow.ItemArray.GetValue(21) + "</lastmod>\n";
                        site_content += "<changefreq>daily</changefreq>\n";
                        site_content += "<priority>0.8</priority>\n";
                        site_content += "</url>\n";
                    }
                }
                catch (Exception ex)
                {
                    admit_card_errorlbl.Text = ex.Message;
                    admit_card_errorlbl.Visible = true;
                }
                con.Close();
                con.Dispose();
                site_content += "</urlset>\n";
                File.WriteAllText(path, String.Empty);
                File.AppendAllText(path, site_content);
                admit_card_errorlbl.Text = "Admit card's sitemap Created Successfully";
                admit_card_errorlbl.Visible = true;
            }
            else
            {
                admit_card_errorlbl.Text = "sitemap-admit-card.xml File not found";
                admit_card_errorlbl.Visible = true;
                return;
            }
        }
        catch (Exception ex)
        {
            admit_card_errorlbl.Text = ex.Message;
            admit_card_errorlbl.Visible = true;
            return;
        }
    }

    protected void answer_key_btn_Click(object sender, EventArgs e)
    {
        answer_key_errorlbl.Visible = false;
        if (answer_key_numbers.Text.Contains(".") == true)
        {
            answer_key_errorlbl.Text = "Invalid total number of answer keys";
            answer_key_errorlbl.Visible = true;
            return;
        }
        if (answer_key_numbers.Text.Contains("-") == true)
        {
            answer_key_errorlbl.Text = "Invalid total number of answer keys";
            answer_key_errorlbl.Visible = true;
            return;
        }
        if (answer_key_numbers.Text.Trim().Length < 1)
        {
            answer_key_errorlbl.Text = "Invalid total number of answer keys";
            answer_key_errorlbl.Visible = true;
            return;
        }
        try
        {
            string path = Server.MapPath("~/sitemap-answer-key.xml");
            if (File.Exists(path))
            {
                string site_content = "";
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string strcon = "select top " + answer_key_numbers.Text.Trim().ToString() + " * from job_site_extra_posts where post_status = 'Published' and post_type='Answer Keys' order by sr DESC";
                SqlCommand cmd = new SqlCommand(strcon, con);
                //cmd.Parameters.AddWithValue("@sr", Request.QueryString["id"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "content");
                site_content = "<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">\n";
                int totalitemsinds = ds.Tables[0].Rows.Count;
                try
                {
                    for (int n = 0; n < totalitemsinds; n++)
                    {
                        DataRow drow = ds.Tables["content"].Rows[n];
                        site_content += "<url>\n";
                        site_content += "<loc>" + drow.ItemArray.GetValue(19) + "</loc>\n";
                        site_content += "<lastmod>" + drow.ItemArray.GetValue(20) + "</lastmod>\n";
                        site_content += "<changefreq>daily</changefreq>\n";
                        site_content += "<priority>0.8</priority>\n";
                        site_content += "</url>\n";
                    }
                }
                catch (Exception ex)
                {
                    answer_key_errorlbl.Text = ex.Message;
                    answer_key_errorlbl.Visible = true;
                }
                con.Close();
                con.Dispose();
                site_content += "</urlset>\n";
                File.WriteAllText(path, String.Empty);
                File.AppendAllText(path, site_content);
                answer_key_errorlbl.Text = "Answer key's Sitemap Created Successfully";
                answer_key_errorlbl.Visible = true;
            }
            else
            {
                answer_key_errorlbl.Text = "sitemap-answer-key.xml File not found";
                answer_key_errorlbl.Visible = true;
                return;
            }
        }
        catch (Exception ex)
        {
            answer_key_errorlbl.Text = ex.Message;
            answer_key_errorlbl.Visible = true;
            return;
        }
    }

    protected void exam_dates_btn_Click(object sender, EventArgs e)
    {
        exam_date_errorlbl.Visible = false;
        if (exam_date_numbers.Text.Contains(".") == true)
        {
            exam_date_errorlbl.Text = "Invalid total number of exam dates";
            exam_date_errorlbl.Visible = true;
            return;
        }
        if (exam_date_numbers.Text.Contains("-") == true)
        {
            exam_date_errorlbl.Text = "Invalid total number of exam dates";
            exam_date_errorlbl.Visible = true;
            return;
        }
        if (exam_date_numbers.Text.Trim().Length < 1)
        {
            exam_date_errorlbl.Text = "Invalid total number of exam dates";
            exam_date_errorlbl.Visible = true;
            return;
        }
        try
        {
            string path = Server.MapPath("~/sitemap-exam-date.xml");
            if (File.Exists(path))
            {
                string site_content = "";
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string strcon = "select top " + exam_date_numbers.Text.Trim().ToString() + " * from job_site_exam_interview where post_status = 'Published' and post_type='Exam Dates' order by sr DESC";
                SqlCommand cmd = new SqlCommand(strcon, con);
                //cmd.Parameters.AddWithValue("@sr", Request.QueryString["id"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "content");
                site_content = "<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">\n";
                int totalitemsinds = ds.Tables[0].Rows.Count;
                try
                {
                    for (int n = 0; n < totalitemsinds; n++)
                    {
                        DataRow drow = ds.Tables["content"].Rows[n];
                        site_content += "<url>\n";
                        site_content += "<loc>" + drow.ItemArray.GetValue(19) + "</loc>\n";
                        site_content += "<lastmod>" + drow.ItemArray.GetValue(20) + "</lastmod>\n";
                        site_content += "<changefreq>daily</changefreq>\n";
                        site_content += "<priority>0.8</priority>\n";
                        site_content += "</url>\n";
                    }
                }
                catch (Exception ex)
                {
                    exam_date_errorlbl.Text = ex.Message;
                    exam_date_errorlbl.Visible = true;
                }
                con.Close();
                con.Dispose();
                site_content += "</urlset>\n";
                File.WriteAllText(path, String.Empty);
                File.AppendAllText(path, site_content);
                exam_date_errorlbl.Text = "Exam date's sitemap created successfully";
                exam_date_errorlbl.Visible = true;
            }
            else
            {
                exam_date_errorlbl.Text = "sitemap-exam-date.xml File not found";
                exam_date_errorlbl.Visible = true;
                return;
            }
        }
        catch (Exception ex)
        {
            exam_date_errorlbl.Text = ex.Message;
            exam_date_errorlbl.Visible = true;
            return;
        }
    }

    protected void interview_schedule_btn_Click(object sender, EventArgs e)
    {
        interview_schedule_errorlbl.Visible = false;
        if (interview_schedule_numbers.Text.Contains(".") == true)
        {
            interview_schedule_errorlbl.Text = "Invalid total number of interview schedules";
            interview_schedule_errorlbl.Visible = true;
            return;
        }
        if (interview_schedule_numbers.Text.Contains("-") == true)
        {
            interview_schedule_errorlbl.Text = "Invalid total number of interview schedules";
            interview_schedule_errorlbl.Visible = true;
            return;
        }
        if (interview_schedule_numbers.Text.Trim().Length < 1)
        {
            interview_schedule_errorlbl.Text = "Invalid total number of jobs";
            interview_schedule_errorlbl.Visible = true;
            return;
        }
        try
        {
            string path = Server.MapPath("~/sitemap-interview-schedule.xml");
            if (File.Exists(path))
            {
                string site_content = "";
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string strcon = "select top " + interview_schedule_numbers.Text.Trim().ToString() + " * from job_site_exam_interview where post_status = 'Published' and post_type='Interview Scheduled' order by sr DESC";
                SqlCommand cmd = new SqlCommand(strcon, con);
                //cmd.Parameters.AddWithValue("@sr", Request.QueryString["id"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "content");
                site_content = "<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">\n";
                int totalitemsinds = ds.Tables[0].Rows.Count;
                try
                {
                    for (int n = 0; n < totalitemsinds; n++)
                    {
                        DataRow drow = ds.Tables["content"].Rows[n];
                        site_content += "<url>\n";
                        site_content += "<loc>" + drow.ItemArray.GetValue(19) + "</loc>\n";
                        site_content += "<lastmod>" + drow.ItemArray.GetValue(20) + "</lastmod>\n";
                        site_content += "<changefreq>daily</changefreq>\n";
                        site_content += "<priority>0.8</priority>\n";
                        site_content += "</url>\n";
                    }
                }
                catch (Exception ex)
                {
                    interview_schedule_errorlbl.Text = ex.Message;
                    interview_schedule_errorlbl.Visible = true;
                }
                con.Close();
                con.Dispose();
                site_content += "</urlset>\n";
                File.WriteAllText(path, String.Empty);
                File.AppendAllText(path, site_content);
                interview_schedule_errorlbl.Text = "Interview schedule's sitemap Created Successfully";
                interview_schedule_errorlbl.Visible = true;
            }
            else
            {
                interview_schedule_errorlbl.Text = "sitemap-interview-schedule.xml File not found";
                interview_schedule_errorlbl.Visible = true;
                return;
            }
        }
        catch (Exception ex)
        {
            interview_schedule_errorlbl.Text = ex.Message;
            interview_schedule_errorlbl.Visible = true;
            return;
        }
    }

    protected void qualification_btn_Click(object sender, EventArgs e)
    {
        qualification_errorlbl.Visible = false;
        if (qualification_numbers.Text.Contains(".") == true)
        {
            qualification_errorlbl.Text = "Invalid total number of qualifications";
            qualification_errorlbl.Visible = true;
            return;
        }
        if (qualification_numbers.Text.Contains("-") == true)
        {
            qualification_errorlbl.Text = "Invalid total number of qualifications";
            qualification_errorlbl.Visible = true;
            return;
        }
        if (qualification_numbers.Text.Trim().Length < 1)
        {
            qualification_errorlbl.Text = "Invalid total number of qualifications";
            qualification_errorlbl.Visible = true;
            return;
        }
        try
        {
            string path = Server.MapPath("~/sitemap-qualification.xml");
            if (File.Exists(path))
            {
                string site_content = "";
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string strcon = "select top " + qualification_numbers.Text.Trim().ToString() + " * from job_site_qualification";
                SqlCommand cmd = new SqlCommand(strcon, con);
                //cmd.Parameters.AddWithValue("@sr", Request.QueryString["id"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "content");
                site_content = "<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">\n";
                int totalitemsinds = ds.Tables[0].Rows.Count;
                try
                {
                    for (int n = 0; n < totalitemsinds; n++)
                    {
                        DataRow drow = ds.Tables["content"].Rows[n];
                        site_content += "<url>\n";
                        site_content += "<loc>https://www.agovtjobs.in/qualifications/" + drow.ItemArray.GetValue(2) + "</loc>\n";
                        site_content += "<lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod>\n";
                        site_content += "<changefreq>daily</changefreq>\n";
                        site_content += "<priority>0.8</priority>\n";
                        site_content += "</url>\n";
                    }
                }
                catch (Exception ex)
                {
                    qualification_errorlbl.Text = ex.Message;
                    qualification_errorlbl.Visible = true;
                }
                con.Close();
                con.Dispose();
                site_content += "</urlset>\n";
                File.WriteAllText(path, String.Empty);
                File.AppendAllText(path, site_content);
                qualification_errorlbl.Text = "Qualification's sitemap Created Successfully";
                qualification_errorlbl.Visible = true;
            }
            else
            {
                qualification_errorlbl.Text = "sitemap-qualification.xml File not found";
                qualification_errorlbl.Visible = true;
                return;
            }
        }
        catch (Exception ex)
        {
            qualification_errorlbl.Text = ex.Message;
            qualification_errorlbl.Visible = true;
            return;
        }
    }

    protected void result_btn_Click(object sender, EventArgs e)
    {
        result_errorlbl.Visible = false;
        if (result_numbers.Text.Contains(".") == true)
        {
            result_errorlbl.Text = "Invalid total number of results";
            result_errorlbl.Visible = true;
            return;
        }
        if (result_numbers.Text.Contains("-") == true)
        {
            result_errorlbl.Text = "Invalid total number of results";
            result_errorlbl.Visible = true;
            return;
        }
        if (result_numbers.Text.Trim().Length < 1)
        {
            result_errorlbl.Text = "Invalid total number of results";
            result_errorlbl.Visible = true;
            return;
        }
        try
        {
            string path = Server.MapPath("~/sitemap-result.xml");
            if (File.Exists(path))
            {
                string site_content = "";
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string strcon = "select top " + result_numbers.Text.Trim().ToString() + " * from job_site_results where post_status = 'Published' ORDER BY sr DESC";
                SqlCommand cmd = new SqlCommand(strcon, con);
                //cmd.Parameters.AddWithValue("@sr", Request.QueryString["id"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "content");
                site_content = "<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">\n";
                int totalitemsinds = ds.Tables[0].Rows.Count;
                try
                {
                    for (int n = 0; n < totalitemsinds; n++)
                    {
                        DataRow drow = ds.Tables["content"].Rows[n];
                        site_content += "<url>\n";
                        site_content += "<loc>" + drow.ItemArray.GetValue(20) + "</loc>\n";
                        site_content += "<lastmod>" + drow.ItemArray.GetValue(21) + "</lastmod>\n";
                        site_content += "<changefreq>daily</changefreq>\n";
                        site_content += "<priority>0.8</priority>\n";
                        site_content += "</url>\n";
                    }
                }
                catch (Exception ex)
                {
                    result_errorlbl.Text = ex.Message;
                    result_errorlbl.Visible = true;
                }
                con.Close();
                con.Dispose();
                site_content += "</urlset>\n";
                File.WriteAllText(path, String.Empty);
                File.AppendAllText(path, site_content);
                result_errorlbl.Text = "Result's sitemap Created Successfully";
                result_errorlbl.Visible = true;
            }
            else
            {
                result_errorlbl.Text = "sitemap-result.xml File not found";
                result_errorlbl.Visible = true;
                return;
            }
        }
        catch (Exception ex)
        {
            result_errorlbl.Text = ex.Message;
            result_errorlbl.Visible = true;
            return;
        }
    }

    protected void syllabus_btn_Click(object sender, EventArgs e)
    {
        syllabus_errorlbl.Visible = false;
        if (syllabus_numbers.Text.Contains(".") == true)
        {
            syllabus_errorlbl.Text = "Invalid total number of syllabus";
            syllabus_errorlbl.Visible = true;
            return;
        }
        if (syllabus_numbers.Text.Contains("-") == true)
        {
            syllabus_errorlbl.Text = "Invalid total number of syllabus";
            syllabus_errorlbl.Visible = true;
            return;
        }
        if (syllabus_numbers.Text.Trim().Length < 1)
        {
            syllabus_errorlbl.Text = "Invalid total number of syllabus";
            syllabus_errorlbl.Visible = true;
            return;
        }
        try
        {
            string path = Server.MapPath("~/sitemap-syllabus.xml");
            if (File.Exists(path))
            {
                string site_content = "";
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string strcon = "select top " + syllabus_numbers.Text.Trim().ToString() + " * from job_site_syllabus where post_status = 'Published' ORDER BY sr DESC";
                SqlCommand cmd = new SqlCommand(strcon, con);
                //cmd.Parameters.AddWithValue("@sr", Request.QueryString["id"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "content");
                site_content = "<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">\n";
                int totalitemsinds = ds.Tables[0].Rows.Count;
                try
                {
                    for (int n = 0; n < totalitemsinds; n++)
                    {
                        DataRow drow = ds.Tables["content"].Rows[n];
                        site_content += "<url>\n";
                        site_content += "<loc>" + drow.ItemArray.GetValue(16) + "</loc>\n";
                        site_content += "<lastmod>" + drow.ItemArray.GetValue(17) + "</lastmod>\n";
                        site_content += "<changefreq>daily</changefreq>\n";
                        site_content += "<priority>0.8</priority>\n";
                        site_content += "</url>\n";
                    }
                }
                catch (Exception ex)
                {
                    syllabus_errorlbl.Text = ex.Message;
                    syllabus_errorlbl.Visible = true;
                }
                con.Close();
                con.Dispose();
                site_content += "</urlset>\n";
                File.WriteAllText(path, String.Empty);
                File.AppendAllText(path, site_content);
                syllabus_errorlbl.Text = "Syllabus's sitemap Created Successfully";
                syllabus_errorlbl.Visible = true;
            }
            else
            {
                syllabus_errorlbl.Text = "sitemap-syllabus.xml File not found";
                syllabus_errorlbl.Visible = true;
                return;
            }
        }
        catch (Exception ex)
        {
            syllabus_errorlbl.Text = ex.Message;
            syllabus_errorlbl.Visible = true;
            return;
        }
    }

    protected void tags_btn_Click(object sender, EventArgs e)
    {
        tag_errorlbl.Visible = false;
        if (tag_numbers.Text.Contains(".") == true)
        {
            tag_errorlbl.Text = "Invalid total number of tag";
            tag_errorlbl.Visible = true;
            return;
        }
        if (tag_numbers.Text.Contains("-") == true)
        {
            tag_errorlbl.Text = "Invalid total number of tag";
            tag_errorlbl.Visible = true;
            return;
        }
        if (tag_numbers.Text.Trim().Length < 1)
        {
            tag_errorlbl.Text = "Invalid total number of tag";
            tag_errorlbl.Visible = true;
            return;
        }
        try
        {
            string path = Server.MapPath("~/sitemap-tag.xml");
            if (File.Exists(path))
            {
                string site_content = "";
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string strcon = "select top " + tag_numbers.Text.Trim().ToString() + " * from job_site_tag";
                SqlCommand cmd = new SqlCommand(strcon, con);
                //cmd.Parameters.AddWithValue("@sr", Request.QueryString["id"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "content");
                site_content = "<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">\n";
                int totalitemsinds = ds.Tables[0].Rows.Count;
                try
                {
                    for (int n = 0; n < totalitemsinds; n++)
                    {
                        DataRow drow = ds.Tables["content"].Rows[n];
                        site_content += "<url>\n";
                        site_content += "<loc>" + drow.ItemArray.GetValue(44) + "</loc>\n";
                        site_content += "<lastmod>" + drow.ItemArray.GetValue(45) + "</lastmod>\n";
                        site_content += "<changefreq>daily</changefreq>\n";
                        site_content += "<priority>0.8</priority>\n";
                        site_content += "</url>\n";
                    }
                }
                catch (Exception ex)
                {
                    tag_errorlbl.Text = ex.Message;
                    tag_errorlbl.Visible = true;
                }
                con.Close();
                con.Dispose();
                site_content += "</urlset>\n";
                File.WriteAllText(path, String.Empty);
                File.AppendAllText(path, site_content);
                tag_errorlbl.Text = "Tag's sitemap Created Successfully";
                tag_errorlbl.Visible = true;
            }
            else
            {
                tag_errorlbl.Text = "sitemap-tag.xml File not found";
                tag_errorlbl.Visible = true;
                return;
            }
        }
        catch (Exception ex)
        {
            tag_errorlbl.Text = ex.Message;
            tag_errorlbl.Visible = true;
            return;
        }
    }

    protected void written_marks_btn_Click(object sender, EventArgs e)
    {
        written_mark_errorlbl.Visible = false;
        if (written_mark_numbers.Text.Contains(".") == true)
        {
            written_mark_errorlbl.Text = "Invalid total number of written marks";
            written_mark_errorlbl.Visible = true;
            return;
        }
        if (written_mark_numbers.Text.Contains("-") == true)
        {
            written_mark_errorlbl.Text = "Invalid total number of written marks";
            written_mark_errorlbl.Visible = true;
            return;
        }
        if (written_mark_numbers.Text.Trim().Length < 1)
        {
            written_mark_errorlbl.Text = "Invalid total number of jobs";
            written_mark_errorlbl.Visible = true;
            return;
        }
        try
        {
            string path = Server.MapPath("~/sitemap-written-mark.xml");
            if (File.Exists(path))
            {
                string site_content = "";
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string strcon = "select top " + written_mark_numbers.Text.Trim().ToString() + " * from job_site_extra_posts where post_status = 'Published' and post_type='Written Marks' order by sr DESC";
                SqlCommand cmd = new SqlCommand(strcon, con);
                //cmd.Parameters.AddWithValue("@sr", Request.QueryString["id"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "content");
                site_content = "<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">\n";
                int totalitemsinds = ds.Tables[0].Rows.Count;
                try
                {
                    for (int n = 0; n < totalitemsinds; n++)
                    {
                        DataRow drow = ds.Tables["content"].Rows[n];
                        site_content += "<url>\n";
                        site_content += "<loc>" + drow.ItemArray.GetValue(19) + "</loc>\n";
                        site_content += "<lastmod>" + drow.ItemArray.GetValue(20) + "</lastmod>\n";
                        site_content += "<changefreq>daily</changefreq>\n";
                        site_content += "<priority>0.8</priority>\n";
                        site_content += "</url>\n";
                    }
                }
                catch (Exception ex)
                {
                    written_mark_errorlbl.Text = ex.Message;
                    written_mark_errorlbl.Visible = true;
                }
                con.Close();
                con.Dispose();
                site_content += "</urlset>\n";
                File.WriteAllText(path, String.Empty);
                File.AppendAllText(path, site_content);
                written_mark_errorlbl.Text = "Written Mark's Sitemap Created Successfully";
                written_mark_errorlbl.Visible = true;
            }
            else
            {
                written_mark_errorlbl.Text = "sitemap-written-mark.xml File not found";
                written_mark_errorlbl.Visible = true;
                return;
            }
        }
        catch (Exception ex)
        {
            written_mark_errorlbl.Text = ex.Message;
            written_mark_errorlbl.Visible = true;
            return;
        }
    }

}