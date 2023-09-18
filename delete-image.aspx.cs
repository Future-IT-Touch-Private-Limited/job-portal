using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adm_delete_image : System.Web.UI.Page
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
            try
            {
                string erouting = Request.QueryString["sr"].ToString();
                if (erouting != null)
                {
                    int inc = 0;
                    DataTable dt = new DataTable();
                    SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                    string strcon = "select * from job_site_images where sr=@sr";
                    SqlCommand cmd = new SqlCommand(strcon, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.Parameters.AddWithValue("@sr", erouting);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "content");
                    DataRow drow = ds.Tables["content"].Rows[inc];
                    Image1.ImageUrl = drow.ItemArray.GetValue(1).ToString();

                    con.Close();
                    con.Dispose();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    protected void deletebtn_Click(object sender, EventArgs e)
    {
        try
        {
            string filePath = Server.MapPath("~/images/" + Request.QueryString["file"].ToString());
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);

                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                string strcon = "delete from job_site_images where filename=@filename and sr=@sr";
                SqlCommand cmd = new SqlCommand(strcon, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@filename", Request.QueryString["file"].ToString());
                cmd.Parameters.AddWithValue("@sr", Request.QueryString["sr"].ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                Response.Redirect("images.aspx");
            }
            else
            {
                Response.Write("error! file not deleted, Possible already deleted");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}