using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;


namespace MySampleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please pass command line arguments.");
                return;
            }

            string output_filepath = args[0];
            string text_to_overlay = args[1];

            byte[] img = await GetImage();

            MemoryStream memStream = new MemoryStream(img);
            Bitmap bitmap = new Bitmap(memStream);
            Graphics g = Graphics.FromImage(bitmap);
            Brush brush = new SolidBrush(Color.SpringGreen);
            Font arial = new Font("Arial", 45, FontStyle.Bold);
            Rectangle rectangle = new Rectangle(150, 150, 350, 100);

            Pen pen = new Pen(Color.White, 3);
            g.DrawRectangle(pen, rectangle);
            g.DrawString(text_to_overlay,arial,brush,rectangle);

            bitmap.Save(output_filepath);


            Console.ReadLine();
        }

        static async Task<byte[]> GetImage()
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetAsync("https://cataas.com/cat");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
                return null;
            }
            
            
        }
    }
}
