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

		void Send(string start, int count, string[] hash)
		{
			_sender.Send(start, count, hash);
		}

		string GetRange(string startNumber)
		{
			
		}
    }
}
