using RequestManager;

namespace Manager
{
	public class Manager
    {
	    private RequestorAdapter _sender;

		Manager(string requestResurce, string replayResurse)
		{
			_sender = new RequestorAdapter(requestResurce, replayResurse);
		}

		string GetRange()
		{
			
		}
    }
}
