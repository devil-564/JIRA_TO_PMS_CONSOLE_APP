using System;
using System.Net;

public class Class1
{
	public Class1()
	{

	}

	public void GETData()
	{
		string api = "https://irfan007lohar.atlassian.net/rest/api/3/user?accountId=712020:ea3704c2-a8a9-4cad-a04b-b5fe4bcbef31";

		WebRequest requestObjectGet = WebRequest.Create(api);

		requestObjectGet.Method = "GET";

		HttpWebResponse responseObjectGet = null;

		responseObjectGet = (HttpWebResponse) requestObjectGet.GetResponse();

		string strResult = null

		(using Stream stream = responseObjectGet.GetResponseStream()){
			
			StreamReader sr = new StreamReader(stream);
			strResult = sr.ReadToEnd();
			sr.Close();

		}


    }
}
