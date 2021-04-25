using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Trying
{

    public partial class Form1 : Form
    {
        //public class AmazonConnectClient : AmazonServiceClient
         //IAmazonConnect, IAmazonService, IDisposable
            public void Dispose()
            {
                throw new NotImplementedException();
            }
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        protected void btnAmazonItemSerach_Click(object sender, EventArgs e)
        {
            string associateId = "**************";
            string accessKey = "*************";
            string secretKey = "**************************";
            string url = "http://webservices.amazon.com/onca/xml";
            HttpWebRequest oRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            oRequest.Method = "GET";
            oRequest.ContentType = "application/xml";
            oRequest.Headers.Add("AWSAccessKeyId", accessKey);
            oRequest.Headers.Add("AssociateTag", associateId);
            oRequest.Headers.Add("Keywords", "Blueant s4");
            oRequest.Headers.Add("Operation", "ItemSearch");
            oRequest.Headers.Add("Service", "AWSECommerceService");
            Encoding oEncod = new UTF8Encoding();
            HMACSHA1 signature = new HMACSHA1();
            signature.Key = oEncod.GetBytes(secretKey);
            string sign = Convert.ToBase64String(signature.Key);
            oRequest.Headers.Add("Signature", sign);
            oRequest.Headers.Add("SearchIndex", "All");
            oRequest.Headers.Add("Version", "2011-08-01");
            string dateval = DateTime.Now.ToString();
            oRequest.Headers.Add("Timestamp", dateval);
            HttpWebResponse oResponse = (HttpWebResponse)oRequest.GetResponse();
            StreamReader oreader = new StreamReader(oResponse.GetResponseStream());
            var val = oreader.ReadToEnd();
            XmlDocument oxmldoc = new XmlDocument();
            oxmldoc.LoadXml(val);
            XmlElement oxmlelem = (XmlElement)oxmldoc.GetElementsByTagName("Items")[0];
            if (oxmlelem != null)
            {
                List<string> olistItems = new List<string>();
                XmlNodeList olist = oxmldoc.GetElementsByTagName("Item");
                for (int i = 0; i < olist.Count; i++)
                {
                    string asi = olist[i].FirstChild.InnerText;
                    olistItems.Add(asi);
                }
            }
        }
    }
}
