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
using System.Net.Mail;
using System.Net;
using System.IO;

public partial class adm_staff_member_add : System.Web.UI.Page
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

    }


    protected void generateaccountbtn_Click(object sender, EventArgs e)
    {
        try
        {
            errorpanel.Visible = false;
            if (name.Text.Trim().ToString()=="")
            {
                errorlbl.Text = "Enter Staff Member Name";
                errorlbl.Visible = true;
                errorpanel.Visible = true;
                return;
            }
            if (username.Text.Contains(" "))
            {
                errorlbl.Text = "Whitespace not allowed in username";
                errorlbl.Visible = true;
                errorpanel.Visible = true;
                return;
            }
            if (username.Text.Length <= 7)
            {
                errorlbl.Text = "Minimum 8 digits username required";
                errorlbl.Visible = true;
                errorpanel.Visible = true;
                return;
            }
            if (password.Text.Contains(" "))
            {
                errorlbl.Text = "Whitespace not allowed in password";
                errorlbl.Visible = true;
                errorpanel.Visible = true;
                return;
            }
            if (password.Text.Length <= 7)
            {
                errorlbl.Text = "Minimum 8 digits password required";
                errorlbl.Visible = true;
                errorpanel.Visible = true;
                return;
            }
            if (password.Text != confirmpassword.Text)
            {
                errorlbl.Text = "Password confirmation failed";
                errorlbl.Visible = true;
                errorpanel.Visible = true;
                return;
            }
            SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
            con.Open();
            SqlCommand cmdd = new SqlCommand("select * from job_site_staff where username = @username", con);
            cmdd.Parameters.AddWithValue("@username", EncryptString(username.Text.Trim().ToString(), EncryptionKey));
            SqlDataReader reader = cmdd.ExecuteReader();
            if (reader.HasRows)
            {
                errorlbl.Text = "Account Already Exist";
                errorlbl.Visible = true;
                return;
            }
            else
            {
                SqlConnection con2 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                SqlCommand cmd2 = new SqlCommand("Insert into job_site_staff (username,password,joindate,name,status) values(@username,@password,@joindate,@name,@status)", con2);
                cmd2.Parameters.AddWithValue("@username", EncryptString(username.Text.Trim().ToString().ToLower(), EncryptionKey));
                cmd2.Parameters.AddWithValue("@password", EncryptString(password.Text.ToString(), EncryptionKey));
                cmd2.Parameters.AddWithValue("@joindate", DateTime.Now.ToString("dd-MM-yyyy"));
                cmd2.Parameters.AddWithValue("@name", name.Text.Trim().ToString());
                cmd2.Parameters.AddWithValue("@status", "Running");
                con2.Open();
                int count2 = cmd2.ExecuteNonQuery();
                con2.Close();
                con2.Dispose();
                Response.Redirect("staff-member-all.aspx");
            }
        }
        catch (Exception ex)
        {

        }
    }
}