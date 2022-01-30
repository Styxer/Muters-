using Munters.Core.Caching;
using Munters.Giphy.Interfaces;
using Munters.Giphy.Manager;
using Munters.Giphy.Model.Parameters;
using Munters.Giphy.Model.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Munters.GUITest
{   

    public partial class Form1 : Form
    {
        private readonly IGiphyManager _giphy;   

        public Form1(IGiphyManager giphy)
        {
            InitializeComponent();

            _giphy = giphy;
           
        }
        private  void Form1_Load(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 50;
        }

        private async void searchBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBoxSearch.Text))
            {
                await Search(); 
            }
            else
            {
                MessageBox.Show(this, "must insert search param", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task Search()
        {
            flowLayoutPanel1.Controls.Clear();
            var searchParameter = new SearchParameter()
            {
                Query = txtBoxSearch.Text
            };

           
            var gifResult =  await _giphy.GifSearch(searchParameter);
            AddGifsToScreen(gifResult);

        }

        private async void btnTrending_Click(object sender, EventArgs e)
        {
            var gifResult = await _giphy.TrendingGifs(new TrendingParameter());
            AddGifsToScreen(gifResult);
        }

        private void AddGifsToScreen(GiphySearchResult gifResult)
        {
            foreach (var item in gifResult.Data)
            {
                var pictureBox = new PictureBox();
                pictureBox.ImageLocation = item.Images.FixedHeight.Url;
                pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                flowLayoutPanel1.Controls.Add(pictureBox);
            }
        }
    }
}
