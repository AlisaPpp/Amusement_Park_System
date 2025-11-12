namespace Amusement_Park_System;

public class Shift
{
    public Shift(DateTime date, DateTime startTime, DateTime endTime)
    {
        Date = date.Date;
        StartTime = startTime;
        EndTime = endTime;

        if (EndTime <= StartTime)
            throw new ArgumentException("Shift EndTime must be after StartTime.");
    }

    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

}