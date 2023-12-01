namespace ApiApp.Pages;

using ApiApp.Models;
using System.Net.Http;
using System.Text.Json;

public partial class AstronomyPicutreOfTheDayPage : ContentPage
{
	public AstronomyPicutreOfTheDayPage()
	{
		InitializeComponent();

		InitializeUI();
	}

	void InitializeUI()
	{
		lblDate.Text = DateTime.Today.ToString("D");

		SetIOTD(DateTime.Today.ToString("yyyy-MM-dd"));
	}

	async void SetIOTD(string date)
	{
		string api_key = "57KgB2vbg68yimzSnLgxGgbzs9DzbnBx53LfC5OT";
        //string api_key = "PCNzlPZn0UbQY5eY5DygqneimX4KebCDRSw68d0x";

        string url = string.Format("https://api.nasa.gov/planetary/apod?date={0}&api_key={1}",
				date,
				api_key);

        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(url)
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine(body);

            ImageOfTheDay? imageOfTheDay =
                JsonSerializer.Deserialize<ImageOfTheDay>(body);

            lblTitle.Text = imageOfTheDay?.title;
            lblCopyright.Text = imageOfTheDay?.copyright;
            lblExplanation.Text = imageOfTheDay?.explanation;
            imgSource.Source = imageOfTheDay.url;
        }
    }

	public record class ImageOfTheDay(
        string? copyright = "no info",
        string? date = "no info",
	    string? explanation = "no info",
        string? hdurl = "no info",
        string? media_type = "no info",
        string? title = "no info",
        string? url = "no info"
	);

    public async void OnConsultButtonClicked(object sender, EventArgs args)
    {
        string api_key = "57KgB2vbg68yimzSnLgxGgbzs9DzbnBx53LfC5OT";
        //string api_key = "PCNzlPZn0UbQY5eY5DygqneimX4KebCDRSw68d0x";

        string url = string.Format("https://api.nasa.gov/planetary/apod?date={0}&api_key={1}",
				datePicker.Date.ToString("yyyy-MM-dd"),
				api_key);

        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(url)
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine(body);

            ImageOfTheDay? imageOfTheDay =
                JsonSerializer.Deserialize<ImageOfTheDay>(body);

            lblTitle.Text = imageOfTheDay?.title;
            lblCopyright.Text = imageOfTheDay?.copyright;
            lblExplanation.Text = imageOfTheDay?.explanation;
            imgSource.Source = imageOfTheDay.url;
        }
    }

    public async void OnNewButtonClicked(object sender, EventArgs args)
    {
        estadoConexion.Text = App.PictureRepo.estadoConexion;
        statusMessage.Text = "";
        await App.PictureRepo.AddNewPictureAsync("string");
        statusMessage.Text = App.PictureRepo.StatusMessage;
    }

    public async void OnGetButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = "";

        List<Picture> picture = await App.PictureRepo.GetAllPictures();
        statusMessage.Text = picture.Count.ToString();
        //pictureList.ItemsSource = picture;
    }
}