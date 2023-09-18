using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Routing;
using System.Text.RegularExpressions;
using System.Configuration.Provider;
using System.Net;
using System.Collections;
using System.Web.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public partial class adm_images : System.Web.UI.Page
{
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    int inc = 0;
    PagedDataSource pgsource = new PagedDataSource();
    int findex, lindex;
    DataRow dr;
    string EncryptionKey = "ravindersinghgodara123admin";
    string EncryptionKey2 = "rvndr@123@adm";

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

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //load state
                try
                {
                    SqlConnection con1 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                    con1.Open();
                    SqlCommand com1 = new SqlCommand("select DISTINCT state from tj_locations order by state", con1); // table name 
                    SqlDataAdapter da1 = new SqlDataAdapter(com1);
                    //com.Parameters.AddWithValue("@statename", choosestateddl.SelectedValue.ToString());
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);  // fill dataset
                    state.DataTextField = ds1.Tables[0].Columns["state"].ToString(); // text field name of table dispalyed in dropdown
                    state.DataSource = ds1.Tables[0];      //assigning datasource to the dropdownlist
                    state.DataBind();  //binding dropdownlist
                    state.Items.Insert(0, "India");
                    con1.Close();
                    con1.Dispose();
                }
                catch (Exception ex)
                {
                }

                SqlConnection con3 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string strcon = "select * from job_site_images ORDER BY sr DESC";
                SqlCommand cmd = new SqlCommand(strcon, con3);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.Parameters.AddWithValue("@epublisheremail", Request.Cookies["li"]["cmpun"].ToString());
                DataSet ds = new DataSet();
                da.Fill(ds, "emp");
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
                con3.Close();
                con3.Dispose();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }



    protected void btnUpload_Click(object sender, EventArgs e)
    {
        Response.AddHeader("X-XSS-Protection", "0");
        string[] validFileTypes = { "png", "jpg", "jpeg", "gif", "webp" };
        string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
        bool isValidFile = false;
        for (int i = 0; i < validFileTypes.Length; i++)
        {
            if (ext == "." + validFileTypes[i])
            {
                isValidFile = true;
                break;
            }
        }
        if (!isValidFile)
        {
            errorlbl.Visible = true;
            errorlbl.Text = "Invalid File. Please upload a File with extension " + string.Join(",", validFileTypes);
            return;
        }
        if (FileUpload1.FileBytes.Length > 1024000)
        {
            errorlbl.Text = "Large file not allowed";
            errorlbl.Visible = true;
            return;
        }

        string filename = DateTime.Now.ToString("ddMMyyyyhhmmss");
        try
        {
            if (FileUpload1.HasFile)
            {
                string exten = Path.GetExtension(FileUpload1.PostedFile.FileName);
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/images/") + filename + exten);

                SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                SqlCommand cmd = new SqlCommand("Insert into job_site_images (logourl,filename,state,job_type) values(@logourl,@filename,@state,@job_type)", con);
                cmd.Parameters.AddWithValue("@logourl", "https://agovtjobs.in/images/" + filename + "" + exten);
                cmd.Parameters.AddWithValue("@filename", filename + "" + exten);
                cmd.Parameters.AddWithValue("@state", state.Text.ToString());
                cmd.Parameters.AddWithValue("@job_type", job_type.Text.ToString());
                con.Open();
                int count = cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}