namespace Radar.Models
{
   public class Flight
{
   
    public double Lat { get; set; }
    public double Lng { get; set; }
    public double Dir { get; set; }
    public double Alt { get; set; }
    public string? Icao {get; set; }
    public string? Dep {get; set;}
    public string? Arr {get;set;}
}
}