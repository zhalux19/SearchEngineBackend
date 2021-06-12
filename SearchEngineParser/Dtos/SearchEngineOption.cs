using System;

namespace SearchEngineParser.Dtos
{
    public class SearchEngineOption
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(Object obj)
        {
            return (obj is SearchEngineOption) && ((SearchEngineOption)obj).Name == Name && ((SearchEngineOption)obj).Id == Id;
        }
    }
}
