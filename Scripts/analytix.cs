using System;

public class Analytix
{
    public int temperature { get; set; }
    public int haptic_intensity { get; set; }
    public int max_flex_range { get; set; }
    public int min_flex_range { get; set; }
    public int avg_flex_range { get; set; }
    public DateTime LastUpdated { get; set; }
    public DateTime Last_Updated { get; internal set; }
}