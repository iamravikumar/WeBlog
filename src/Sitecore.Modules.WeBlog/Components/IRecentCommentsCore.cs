using System.Collections.Generic;
using Sitecore.Modules.WeBlog.Data.Items;
using Sitecore.Modules.WeBlog.Model;

namespace Sitecore.Modules.WeBlog.Components
{
    public interface IRecentCommentsCore
    {
        IList<EntryComment> Comments { get; }

        void Initialise();
    }
}