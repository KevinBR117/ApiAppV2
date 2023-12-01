using System.Text.Json;
using System.Collections.Generic;
namespace ApiApp.Pages;

public partial class InterplanetaryShock : ContentPage
{
	public InterplanetaryShock()
	{
		InitializeComponent();

		InitializeUI();
	}
	void InitializeUI()
	{
		entInicio.Text = "2023-11-01";
		entFinal.Text = DateTime.Today.ToString("yyyy-MM-dd");
	}

	public async void OnConsultarButtonClicked(object sender, EventArgs e)
	{
		string url = string.Format("https://kauai.ccmc.gsfc.nasa.gov/DONKI/WS/get/IPS?startDate={0}&endDate={1}&location=Earth",
			entInicio.Text,
			entFinal.Text);

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
			var interShock = JsonSerializer.Deserialize<List<InterShock>>(body);
			Console.WriteLine(body);
			Console.WriteLine(interShock[0]);
			Console.WriteLine(interShock[0].catalog);
			Console.WriteLine(interShock[0].instruments[0].displayName);
			int col = 0;
			int ren = 0;
			for(int i = 0; i < interShock.Count; i++)
			{
                gridInterplanetaryShock.RowDefinitions.Add(new RowDefinition { });
                gridInterplanetaryShock.ColumnDefinitions.Add(new ColumnDefinition { });

				gridInterplanetaryShock.Add(new Label { Text = string.Format("catalog: {0}", interShock[i].catalog) }, col, ren);
				ren++;
				gridInterplanetaryShock.Add(new Label { Text = string.Format("location: {0}", interShock[i].location) }, col, ren);
				ren++;
                gridInterplanetaryShock.Add(new Label { Text = string.Format("eventTime: {0}", DateTime.Parse(interShock[i].eventTime).ToString("MM/dd/yyyy HH:mm")) }, col, ren);
                ren++;

                for (int j = 0; j < interShock[0].instruments.Count; j++)
				{
					gridInterplanetaryShock.RowDefinitions.Add(new RowDefinition { });
					gridInterplanetaryShock.ColumnDefinitions.Add(new ColumnDefinition { });
					gridInterplanetaryShock.Add(new Label { Text = string.Format("displayName: {0}", interShock[i].instruments[j].displayName) }, col, ren);
					ren++;
				}
				ren++;
			}
		}
	}

	public class InterShock
	{
		public string catalog { get; set; }
		public string location { get; set; }
		public string eventTime { get; set; }
		public List<Instruments> instruments { get; set; }
	}

	public class Instruments
	{
		public string displayName { get; set; }
	}
}