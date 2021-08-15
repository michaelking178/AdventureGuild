using System;

[Serializable]
public class Artisan : Vocation
{
    public int ProjectsCompleted { get; set; } = 0;

    public Artisan()
    {
        title = "Artisan";
        MaxLevel = 10;
    }
}
