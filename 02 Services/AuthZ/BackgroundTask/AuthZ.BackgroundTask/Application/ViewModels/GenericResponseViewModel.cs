using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace AuthZ.BackgroundTask.Application.ViewModels
{
    [DataContract]
    public class GenericResponseViewModel
    {     

        public GenericResponseViewModel()
        {
            Messages = new List<GenericMessageResponseViewModel>();
        }

        public bool HasError()
        {
            return (Messages != null && Messages.Any());
        }


        [DataMember] public List<GenericMessageResponseViewModel> Messages { get; set; }



        [DataMember] public GenericMessageType Type { get; set; }

        protected GenericResponseViewModel(string message , GenericMessageType type = GenericMessageType.Info) : this()
        {
            Type = type;
            Messages.Add(new GenericMessageResponseViewModel(type, message));

        }

    }


    public class GenericMessageResponseViewModel
    {

        [DataMember] public string Messages { get; set; }
        [DataMember] public GenericMessageType Type { get; set; }

        public GenericMessageResponseViewModel(GenericMessageType type, string message)
        {
            Type = type;
            Messages = message;
        }
    }


    [DataContract]
    public enum GenericMessageType
    {
        [EnumMember] Info = 1,
        [EnumMember] Warning = 2,
        [EnumMember] Error = 3,
    }
}
