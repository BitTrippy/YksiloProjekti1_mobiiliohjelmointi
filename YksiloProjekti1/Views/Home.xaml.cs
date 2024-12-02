using System.Text.Json;
using Microsoft.Maui.Devices.Sensors;
using YksiloProjekti1.Services;

namespace YksiloProjekti1.Views;

public partial class Home : ContentPage
{

    private JsonDatabase _jsonDatabase;
    private DateTime _lastRandomizeTime;
    private readonly TimeSpan _randomizeInterval = TimeSpan.FromSeconds(2);

    public Home()
	{
        InitializeComponent();
        _jsonDatabase = new JsonDatabase();
        FetchJsonData();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Gyroscope.ReadingChanged += Gyroscope_ReadingChanged;
        Gyroscope.Start(SensorSpeed.UI);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Gyroscope.ReadingChanged -= Gyroscope_ReadingChanged;
        Gyroscope.Stop();
    }

    private void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e)
    {
        var data = e.Reading;
        Console.WriteLine(data);

        var y = data.AngularVelocity.Y;

        if (y > 3)
        {
            RandomizeResponse(sender, e);
        }
    }


    private async void FetchJsonData()
    {
        try
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("data.json");
            using var reader = new StreamReader(stream);

            var json = await reader.ReadToEndAsync();
            Console.WriteLine(json);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<List<JsonResponse>>(json, options);

            _jsonDatabase.Responses = data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }


    private void RandomizeResponse(object sender, EventArgs e)
    {
        if (DateTime.Now - _lastRandomizeTime < _randomizeInterval)
        {
            return;
        }

        _lastRandomizeTime = DateTime.Now;

        var random = new Random();
        var index = random.Next(0, _jsonDatabase.Responses.Count);
        var response = _jsonDatabase.Responses[index];

        AnswerBox.Text = response.Response;
    }
}