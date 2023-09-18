using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class adm_tags : System.Web.UI.Page
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
            loadrepeater();
        }
    }

    public void loadrepeater()
    {
        try
        {
            SqlConnection con3 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
            string strcon = "select * from job_site_tag ORDER BY sr DESC";
            SqlCommand cmd = new SqlCommand(strcon, con3);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "emp");
            Repeater1.DataSource = ds;
            Repeater1.DataBind();
            con3.Close();
            con3.Dispose();
        }
        catch (Exception ex)
        {
        }
    }

    protected void savebtn_Click(object sender, EventArgs e)
    {
        try
        {
            errorpanel.Visible = false;
            SqlConnection con = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
            con.Open();
            SqlCommand cmdd = new SqlCommand("select * from job_site_tag where tag = @tag", con);
            cmdd.Parameters.AddWithValue("@tag", valuetxt.Text.Trim().ToString());
            SqlDataReader reader = cmdd.ExecuteReader();

            if (reader.HasRows)
            {
                errorlbl.Text = "Tag Already Exist";
                errorpanel.Visible = true;
                errorlbl.Visible = true;
                return;
            }
            else
            {
                string ipaddress;
                ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipaddress == "" || ipaddress == null)
                    ipaddress = Request.ServerVariables["REMOTE_ADDR"];

                string strquery = valuetxt.Text.Trim().ToLower();
                strquery = strquery.Replace(" ", "").Replace("`", "").Replace("~", "").Replace(".", "").Replace(",", "").Replace("/", "").Replace(";", "").Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace("+", "").Replace("=", "").Replace("?", "").Replace("&", "").Replace("#", "").Replace("^", "").ToString();

                SqlConnection con2 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                SqlCommand cmd2 = new SqlCommand("Insert into job_site_tag (tag,query) values(@tag,@query)", con2);
                cmd2.Parameters.AddWithValue("@tag", valuetxt.Text.Trim().ToString());
                cmd2.Parameters.AddWithValue("@query", strquery.Trim());
                con2.Open();
                int count2 = cmd2.ExecuteNonQuery();
                con2.Close();
                con2.Dispose();
                valuetxt.Text = "";
            }
            loadrepeater();
        }
        catch (Exception ex)
        {
            errorlbl.Text = ex.Message;
            errorpanel.Visible = true;
            errorlbl.Visible = true;
        }
    }

    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case "deletebtn":
                    errorpanel.Visible = false;
                    Label srlbl = (Label)e.Item.FindControl("deletesrlbl");
                    string srvalue = srlbl.Text.ToString();
                    SqlConnection con2 = new SqlConnection(DecryptString(System.Configuration.ConfigurationManager.AppSettings["cn"], EncryptionKey2));
                    string strcon2 = "delete from job_site_tag where sr=@sr";
                    SqlCommand cmd2 = new SqlCommand(strcon2, con2);
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    cmd2.Parameters.AddWithValue("@sr", srvalue);
                    con2.Open();
                    cmd2.ExecuteNonQuery();
                    con2.Close();
                    con2.Dispose();
                    break;
            }
            loadrepeater();
        }
        catch (Exception ex)
        {
            errorpanel.Visible = true;
            errorlbl.Text = ex.Message;
        }
    }

}