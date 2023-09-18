using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;

public partial class adm_download_data : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void downloadlastpost_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Jobs " + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            //GridView1.GridLines = GridLines.Both;
            GridView1.HeaderStyle.Font.Bold = true;
            GridView1.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            statuslbl.Text = ex.Message;
        }
    }


    protected void viewpostbtn_Click(object sender, EventArgs e)
    {
        try
        {
            if (downloadlatesttxt.Text.Trim().ToString() == "")
            {
                statuslbl.Text = "No post to show";
                return;
            }
            SqlConnection con3 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
            string viewUpto = downloadlatesttxt.Text.Trim().ToString();
            string strcon = "select top " + viewUpto + " sr,state,district,area,pincode,main_category,sch_industry,sch_post_name,sch_number_of_posts,sch_qualification,sch_valid_through,pdf_url,sch_apply_now_url,sch_salery,sch_apply_now_url,post_published from job_site_posts ORDER BY sr DESC";
            SqlCommand cmd = new SqlCommand(strcon, con3);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "emp");
            GridView1.DataSource = ds;
            GridView1.DataBind();
            con3.Close();
            con3.Dispose();
            statuslbl.Text = "Showing <strong>" + downloadlatesttxt.Text.Trim() + "</strong> latest posts";
        }
        catch (Exception ex)
        {
            statuslbl.Text = ex.Message;
        }
    }

    protected void viewbydatebtn_Click(object sender, EventArgs e)
    {
        try
        {
            int countuser01 = 0;
            try
            {
                SqlConnection con01 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string myScalarQuery01 = "select count(*) from job_site_posts where post_published like '%'+@post_published+'%'";
                SqlCommand myCommand01 = new SqlCommand(myScalarQuery01, con01);
                myCommand01.Connection.Open();
                myCommand01.Parameters.AddWithValue("@post_published", viewbydatetxt.Text.ToString());
                countuser01 = (int)myCommand01.ExecuteScalar();
                con01.Close();
                con01.Dispose();
            }
            catch (Exception ex)
            {
                countuser01 = 0;
            }
            SqlConnection con3 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
            string viewUpto = downloadlatesttxt.Text.Trim().ToString();
            string strcon = "select sr,state,district,area,pincode,main_category,sch_industry,sch_post_name,sch_number_of_posts,sch_qualification,sch_valid_through,pdf_url,sch_apply_now_url,sch_salery,post_published from job_site_posts where post_published like '%'+@post_published+'%' ORDER BY sr DESC";
            SqlCommand cmd = new SqlCommand(strcon, con3);
            cmd.Parameters.AddWithValue("@post_published", viewbydatetxt.Text.ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "emp");
            GridView1.DataSource = ds;
            GridView1.DataBind();
            con3.Close();
            con3.Dispose();
            statuslbl.Text = "Jobs list published on <strong>" + viewbydatetxt.Text.Trim() + "</strong>, Total (<strong>"+ countuser01 + "</strong>) ";
        }
        catch (Exception ex)
        {
            statuslbl.Text = ex.Message;
        }
    }
    
    protected void viewsubmittedbtn_Click(object sender, EventArgs e)
    {
        try
        {
            int countuser01 = 0;
                string datefromtxtbox = viewbydatetxt.Text.ToString();
                char[] spearator = { '-' };
                String[] strlist = datefromtxtbox.Split(spearator);
                string splittedDate = strlist[2] + "-" + strlist[1] + "-" + strlist[0];

            try
            {
                SqlConnection con01 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string myScalarQuery01 = "select count(*) from job_site_posts where submitted_date like '%'+@submitted_date+'%'";
                SqlCommand myCommand01 = new SqlCommand(myScalarQuery01, con01);
                myCommand01.Connection.Open();
                myCommand01.Parameters.AddWithValue("@submitted_date", splittedDate);
                countuser01 = (int)myCommand01.ExecuteScalar();
                con01.Close();
                con01.Dispose();
            }
            catch (Exception ex)
            {
                countuser01 = 0;
            }
            SqlConnection con3 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
            string viewUpto = downloadlatesttxt.Text.Trim().ToString();
            string strcon = "select sr,state,district,area,pincode,main_category,sch_industry,sch_post_name,sch_number_of_posts,sch_qualification,sch_valid_through,pdf_url,sch_apply_now_url,sch_salery,post_published,submitted_date from job_site_posts where submitted_date like '%'+@submitted_date+'%' ORDER BY sr DESC";
            SqlCommand cmd = new SqlCommand(strcon, con3);
            cmd.Parameters.AddWithValue("@submitted_date", splittedDate);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "emp");
            GridView1.DataSource = ds;
            GridView1.DataBind();
            con3.Close();
            con3.Dispose();
            statuslbl.Text = "Jobs list Submitted on <strong>" + viewbydatetxt.Text.Trim() + "</strong>, Total (<strong>" + countuser01 + "</strong>) ";
        }
        catch (Exception ex)
        {
            statuslbl.Text = ex.Message;
        }
    }

}