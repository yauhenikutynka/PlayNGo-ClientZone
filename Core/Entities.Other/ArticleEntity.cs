using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 文章实体(XML & 序列化)
    /// </summary>
    [Serializable]
    [DataObject]
    [Description("文章")]
    [XmlEntityAttributes("Playngo_ClientZone//EventList//EventItem")]
    public class EventEntity
    {


        #region 属性
        /// <summary>文章标题</summary>
        public String Title { get; set; }

        /// <summary>文章摘要</summary>
        public String Summary { get; set; }

        /// <summary>文章内容</summary>
        public String ContentText { get; set; }

        /// <summary>颜色</summary>
        public String Color { get; set; }
        

        /// <summary>文章分类</summary>
        public String Categories { get; set; }

        /// <summary>附属图片(媒体路径)</summary>
        public String Picture { get; set; }

        /// <summary>附属图片(媒体名称)</summary>
        public String PictureName { get; set; }

        /// <summary>文章状态(草稿、正式、锁定、删除)</summary>
        public Int32 Status { get; set; }

        /// <summary>文章归属(公共、私有：私有的文章必须创建者为自己才可以设置)</summary>
        public Int32 AttributionStatus { get; set; }

        /// <summary>文章置顶(正常、置顶、描红)</summary>
        public Int32 TopStatus { get; set; }

        /// <summary>文章标签集合</summary>
        public String Tags { get; set; }

        /// <summary>评论数量</summary>
        public Int32 RegisteredNumber { get; set; }

        /// <summary>总评分数</summary>
        public Int32 TotalScore { get; set; }

        /// <summary>总评分人数</summary>
        public Int32 TotalScoreNumber { get; set; }

        /// <summary>浏览数量</summary>
        public Int32 ViewCount { get; set; }

        /// <summary>发布时间</summary>
        public DateTime StartTime { get; set; }

        /// <summary>结束时间</summary>
        public DateTime EndTime { get; set; }

        /// <summary>创建</summary>
        public DateTime CreateTime { get; set; }
 
        /// <summary>特征</summary>
        public Int32 Feature { get; set; }

        /// <summary>发送订阅通知(0:未发,1:已发)</summary>
        public Int32 SendSubscribe { get; set; }
        

        /// <summary>搜索标题</summary>
        public String SearchTitle { get; set; }

        /// <summary>搜索关键字</summary>
        public String SearchKeywords { get; set; }

        /// <summary>搜索描述</summary>
        public String SearchDescription { get; set; }

        /// <summary>来源</summary>
        public String Source { get; set; }

        /// <summary>选项集合</summary>
        public String Options { get; set; }

        /// <summary>扩展选项(用户自定义)</summary>
        public String Extension { get; set; }

      

        /// <summary>所有用户可见</summary>
        public int Per_AllUsers { get; set; }

        /// <summary>可见权限角色集合</summary>
        public String Per_Roles { get; set; }

        /// <summary>地点坐标</summary>
        public String LocationXY { get; set; }

        /// <summary>地点</summary>
        public String Location { get; set; }

        /// <summary>伪静态</summary>
        public String FriendlyUrl { get; set; }
        

        /// <summary>相册集</summary>
        public String Gallerys { get; set; }


        /// <summary>附件集</summary>
        public String Attachments { get; set; }

        /// <summary>循环记录</summary>
        public String Repeats { get; set; }
        


        ///<summary>
        /// 整天
        ///</summary>
        public Int32 AllDay { get; set; }

        ///<summary>
        /// 开始日期
        ///</summary>
        public DateTime StartDateTime { get; set; }


        ///<summary>
        /// 结束日期
        ///</summary>
        public DateTime EndDateTime { get; set; }



        ///<summary>
        /// 重复类型
        ///</summary>
        public Int32 Repeat { get; set; }

        ///<summary>
        /// 结束重复类型
        ///</summary>
        public Int32 EndRepeat { get; set; }

        ///<summary>
        /// 重复结束时间
        ///</summary>
        public DateTime EndRepeatDate { get; set; }

        ///<summary>
        /// 重复的次数
        ///</summary>
        public Int32 EndRepeatCount { get; set; }

        ///<summary>
        /// 每次重复类型(年月周日)
        ///</summary>
        public Int32 RepeatEvery { get; set; }

        ///<summary>
        /// 循环区间
        ///</summary>
        public Int32 RepeatEveryInterval { get; set; }

        ///<summary>
        /// 往后几个月
        ///</summary>
        public  String FollowMonths { get; set; }

        ///<summary>
        /// 一年中的第几周
        ///</summary>
        public  String FollowWeekOfYear { get; set; }

        ///<summary>
        /// 一月中的第几日
        ///</summary>
        public  String FollowDaysOfMonth { get; set; }

        ///<summary>
        /// 一周中的第几天
        ///</summary>
        public  String FollowDaysOfWeek { get; set; }

        ///<summary>
        /// 小时分循环
        ///</summary>
        public Int32 FollowSeveralTimes { get; set; }

        ///<summary>
        /// 小时循环
        ///</summary>
        public  String FollowHours { get; set; }

        ///<summary>
        /// 分钟循环
        ///</summary>
        public  String FollowMinutes { get; set; }
        #endregion

    }
}