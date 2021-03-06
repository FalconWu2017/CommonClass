﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonClass.DataStructure
{
    /// <summary>
    /// 泛型委托，比较一个节点是否为另一个节点的子节点
    /// </summary>
    /// <typeparam name="T">节点的数据类型</typeparam>
    /// <param name="father">可能的父节点</param>
    /// <param name="child">可能的子节点</param>
    /// <returns>如果True将作为子节点，否则不是</returns>
    public delegate bool IsChild<T>(T father,T child);

    /// <summary>
    /// 树帮助类。
    /// </summary>
    /// <typeparam name="T">树的节点类型</typeparam>
    public class Tree<T>
    {
        /// <summary>
        /// 将列表数据转换为树数据
        /// </summary>
        /// <param name="list">数据列表</param>
        /// <param name="isChild">判断一个节点是否为另一个节点的子节点</param>
        /// <param name="isRoot">判断一个节点是否为根节点</param>
        /// <returns></returns>
        public virtual TreeRoot<T> FromList(IEnumerable<T> list,IsChild<T> isChild,Func<T,bool> isRoot) {
            var root = new TreeRoot<T> { Children = new List<TreeNode<T>>() };
            list = list.Where(m => m != null);
            var nodeList = new List<TreeNode<T>>();
            nodeList.AddRange(list.Select(m => new TreeNode<T> {
                Children = new List<TreeNode<T>>(),
                Data = m,Father = null,
            }));
            nodeList.ForEach(m => {
                if(isRoot(m.Data)) {
                    root.Children.Add(m);
                }
            });
            root.Children.ForEach(m => fromList1(list,m,isChild));
            return root;
        }


        /// <summary>
        /// 将列表数据转换为树数据
        /// </summary>
        /// <param name="list">列表数据</param>
        /// <param name="root">一个根，如果传入null，返回一个新的根。</param>
        /// <param name="isChild">判断第二个对象是否为第一个对象子节点的方法</param>
        /// <returns>树根</returns>
        private TreeNode<T> fromList1(IEnumerable<T> list,TreeNode<T> root,IsChild<T> isChild) {
            foreach(var i in list) {
                if(root.Data != null && root.Data.Equals(i)) {
                    continue;
                }
                if(isChild(root.Data,i)) {
                    var tempList = list.Where(m => !m.Equals(i));
                    fromList1(tempList,AddChild(root,i),isChild);
                }
            }
            return root;
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="father">父节点</param>
        /// <param name="data">子节点的数据</param>
        /// <returns>添加的子节点</returns>
        public virtual TreeNode<T> AddChild(TreeNode<T> father,T data) {
            var node = new TreeNode<T> { Data = data,Father = father,Children = new List<TreeNode<T>>(),};
            father.Children.Add(node);
            return node;
        }
    }
    /// <summary>
    /// 树节点
    /// </summary>
    /// <typeparam name="T">节点数据类型</typeparam>
    public class TreeNode<T>
    {
        /// <summary>
        /// 节点保存的数据
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 树父节点
        /// </summary>
        public TreeNode<T> Father { get; set; }
        /// <summary>
        /// 树子节点
        /// </summary>
        public List<TreeNode<T>> Children { get; set; }
        /// <summary>
        /// 将节点转换为枚举形态。
        /// </summary>
        /// <returns>一个数据列表</returns>
        public IEnumerable<T> ToList() {
            var result = new List<T>();
            if(this.Children == null || this.Children.Count == 0) {
                return result;
            }
            foreach(var c in this.Children) {
                result.Add(c.Data);
                result.AddRange(c.ToList());
            }
            return result;
        }

    }
    /// <summary>
    /// 表示树根节点
    /// </summary>
    /// <typeparam name="T">树的数据类型</typeparam>
    public class TreeRoot<T>
    {
        /// <summary>
        /// 树根的第一层子节点
        /// </summary>
        public List<TreeNode<T>> Children { get; set; }
        /// <summary>
        /// 将整颗树转换为枚举形式
        /// </summary>
        /// <returns>树包含的数据枚举</returns>
        public IEnumerable<T> ToList() {
            var result = new List<T>();
            foreach(var n in Children) {
                result.Add(n.Data);
                result.AddRange(n.ToList());
            }
            return result;
        }

    }
}
