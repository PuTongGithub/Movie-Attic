using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MovieCollection.data
{
    public class CollectedMovieTagList
    {
        public static List<string> TagList;
        private static string TagListFileName = "TagList.data";
        private static StorageFile file;

        public async static void GetTagList()
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            file = await folder.CreateFileAsync(TagListFileName, CreationCollisionOption.OpenIfExists);
            var iTagList = await FileIO.ReadLinesAsync(file);
            TagList = new List<string>();
            if (iTagList != null && iTagList.Count() >= 1)
            {
                for (int i = 0; i < iTagList.Count(); i++)
                {
                    var item = iTagList[i];
                    if (item != null)
                    {
                        TagList.Add(item);
                    }
                }
            }
        }

        public async static void AddTags(List<string> NewTags)
        {
            foreach(var item in NewTags)
            {
                TagList.Add(item);
            }
            TagList.Sort();
            await FileIO.WriteLinesAsync(file, TagList);
        }

        public async static void SubTag(string OldTag)
        {
            var result = TagList.Remove(OldTag);
            if (result)
            {
                await FileIO.WriteLinesAsync(file, TagList);
            }
            else
            {
                return;
            }
        }
    }

}
