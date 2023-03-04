namespace Perseverance.Shared.Models.SnailyCAD
{
    public class ErrorMessage
    {
        public string name { get; set; }
        public string message { get; set; }
        public int status { get; set; }
        // public List<dynamic> errors { get; set; }

        /*
         * 
         * TODO: Handle errors object
         * Object; errors: [ { "username": "Required", "password": "Required" } ]
         * 
         * This doesn't like being handled in the MsgPackSerializer
         * its like a List<KeyValuePair<string, string>> but it doesn't like that either
         * I've tried List<Dictionary<string, string>> and IDictionary<string, string>
         *
         */

    }
}
