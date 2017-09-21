using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using EasyRbac.Utils;
using EasyRbac.Utils.Collections;
using Xunit;

namespace EasyRbac.Test
{
    public class MultiTreeHelperTest
    {
        [Fact]
        public void GenerateTree_Success()
        {
            var source = this.GetMockData();
            var result = source.ToToMultiTree(x => x.Id, x => x.Id.Substring(0, x.Id.Length - 2));
            Assert.Equal(result.Count,1);
            Assert.Equal(result[0].Children.Count,4);

        }

        [Fact]
        public void Generate_ParentIdTree_Success()
        {
            var source = this.GetParentIdMockData();
            var result = source.ToToMultiTree(x => x.Id, x => x.ParentId);
            Assert.Equal(result.Count, 2);
            Assert.Equal(result[0].Children.Count,3);

        }

        private List<PrefixTreeNode> GetMockData()
        {
            var arr = new[]
            {
                new PrefixTreeNode("11"),

                new PrefixTreeNode("1105"),
                new PrefixTreeNode("110501"),
                new PrefixTreeNode("11050101"),

                new PrefixTreeNode("1122"),
                new PrefixTreeNode("112201"),

                new PrefixTreeNode("1123"),
                new PrefixTreeNode("112331"),

                new PrefixTreeNode("1133"),
                
            };

            return arr.ToList();
        }

        private List<ParentIdTreeNode> GetParentIdMockData()
        {
            var arr = new[]
            {
                new ParentIdTreeNode(1, 0),
                new ParentIdTreeNode(2, 1),
                new ParentIdTreeNode(3, 1),
                new ParentIdTreeNode(4, 1),
                new ParentIdTreeNode(5, 2),
                new ParentIdTreeNode(6, 5),
                new ParentIdTreeNode(7, 2),
                new ParentIdTreeNode(8, 2),
                new ParentIdTreeNode(9, 6),
                new ParentIdTreeNode(10, 5),
                new ParentIdTreeNode(11, 0),
            };
            return arr.ToList();
        }
    }
    [DebuggerDisplay("ID={Id}")]
    class PrefixTreeNode : IMultiTree<PrefixTreeNode>
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public PrefixTreeNode(string id)
        {
            this.Id = id;
        }

        public string Id { get; set; }

        public List<PrefixTreeNode> Children { get; set; }
    }

    [DebuggerDisplay("ID={Id},ParentId={ParentId}")]
    class ParentIdTreeNode : IMultiTree<ParentIdTreeNode>
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public ParentIdTreeNode(int id,int parentId)
        {
            this.Id = id;
            this.ParentId = parentId;
        }

        public int Id { get; set; }

        public int ParentId { get; set; }

        public List<ParentIdTreeNode> Children { get; set; }
    }
}
