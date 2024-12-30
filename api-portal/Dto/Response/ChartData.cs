namespace api_portal.Dto.Response
{
    public class ChartData
    {
        public List<string> Labels { get; set; }
        public List<ChartDataset> Datasets { get; set; }
    }
}
