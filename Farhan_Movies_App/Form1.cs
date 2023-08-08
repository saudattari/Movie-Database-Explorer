using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farhan_Movies_App
{
    public partial class Form1 : Form
    {
        private string ApiKey = "b544fe0c0349f5d37a7ee3fd7b320d1e";
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string search = textbox1.Text;
            if(string.IsNullOrEmpty(search) )
            {
                MessageBox.Show("Fill it");
                return;
            }
            try
            {
                using(WebClient webClient = new WebClient())
                {
                string url = $"https://api.themoviedb.org/3/search/movie?api_key={ApiKey}&query={search}";
                    string json = webClient.DownloadString(url);
                    dynamic result = JsonConvert.DeserializeObject(json);
                    string poster_post = result.results[0].poster_path;
                    string moviename = result.results[0].title;
                    string overview = result.results[1].overview;
                    string release_date = result.results[1].release_date;
                    label2.Text = $"{moviename}";
                    richTextBox1.Text = $"{overview}";
                    label6.Text = $"{release_date}";
                    string poster_url = $"https://image.tmdb.org/t/p/w500{poster_post}";

                    pictureBox1.Image = DownloadImageFromUrl(poster_url);
                  
                }

            }
            catch( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Image DownloadImageFromUrl(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                byte[] imageData = webClient.DownloadData(url);
                using (var stream = new System.IO.MemoryStream(imageData))
                {
                    return Image.FromStream(stream);
                }
            }
        }
    }
}
