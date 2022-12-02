using dflConverter.impl;
using dflConverter.impl.Decoder;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dflConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            string inputText = inputTextBox.Text;
            outputTextBox.Text = DFL.convertCode(inputText).ToString();


            // https://gigi.nullneuron.net/gigilabs/compressing-strings-using-gzip-in-c/
            string command = "/give @p minecraft:ender_chest{PublicBukkitValues:{\"hypercube:codetemplatedata\":'{\"author\":\"DFL\",\"name\":\"§b§lFunction §3» §bUnnamed\",\"version\":1,\"code\":\"INSERTHERE\"}'},display:{Name:'{\"extra\":[{\"bold\":true,\"italic\":false,\"underlined\":false,\"strikethrough\":false,\"obfuscated\":false,\"color\":\"aqua\",\"text\":\"Function \"},{\"bold\":false,\"italic\":false,\"color\":\"dark_aqua\",\"text\":\"» \"},{\"italic\":false,\"color\":\"aqua\",\"text\":\"Unnamed\"}],\"text\":\"\"}'}} 1";
            
            gzipOutput.Text = command.Replace("INSERTHERE", encode(outputTextBox.Text));
        }
        private void decompressButton_Click(object sender, EventArgs e)
        {
            string txt = decompressText.Text;

            string gzEncoded = txt;
            if (txt.Contains("\"code\":"))
            {
                if (!txt.StartsWith("\'")) txt = "'" + txt + "'";
                gzEncoded = txt.Substring(txt.IndexOf("\"code\":") + 8);
                gzEncoded = gzEncoded.Remove(gzEncoded.Length - 3);
            }

            try
            {
                string jsonString = decode(gzEncoded);

                JObject jsonDecoded = JObject.Parse(jsonString);

                if (noParseToggle.Checked)
                {
                    inputTextBox.Text = jsonDecoded.ToString();
                }
                else
                {
                    inputTextBox.Text = CodeDecoder.Decode(jsonDecoded);
                }
            } catch {}
        }

        private string encode(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            using (var outputStream = new MemoryStream())
            {
                using (var gZipStream = new GZipStream(outputStream, CompressionMode.Compress))
                    gZipStream.Write(inputBytes, 0, inputBytes.Length);

                var outputBytes = outputStream.ToArray();

                var outputbase64 = Convert.ToBase64String(outputBytes);
                return outputbase64;
            }
        }
        private string decode(string input)
        {
            byte[] inputBytes = Convert.FromBase64String(input);

            using (var inputStream = new MemoryStream(inputBytes))
            using (var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress))
            using (var streamReader = new StreamReader(gZipStream))
            {
                var decompressed = streamReader.ReadToEnd();
                return decompressed;
            }
        }


    }
}
