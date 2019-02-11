using CommonLangLib;

namespace PreProcessor
{
    public class PreProcessor : IFileProcessor<PageInfo, PageInfo>
    {
        public PageInfo Execute(PageInfo input)
        {
            return input;
        }
    }
}
