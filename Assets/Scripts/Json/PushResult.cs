
[System.Serializable]
public class CResult
{
	public string title;
	public string description;
	public string clickurl;
	public string icon;
	public string image;
	public double bid;
}
 
[System.Serializable]
public class CPushResult
{
	public double execution_time;
	public CResult[] results;
}
