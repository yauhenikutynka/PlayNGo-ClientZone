using System;
using System.Collections.Generic;
using System.Web;



namespace Playngo.Modules.ClientZone
{

    /// <summary>
    /// 移动类型
    /// </summary>
    public enum EnumMoveType
    {
        /// <summary>
        /// Up
        /// </summary>
        [Text("Up")]
        Up,
        /// <summary>
        /// Down
        /// </summary>
        [Text("Down")]
        Down,
        /// <summary>
        /// Top
        /// </summary>
        [Text("Top")]
        Top,
        /// <summary>
        /// Bottom
        /// </summary>
        [Text("Bottom")]
        Bottom,
        /// <summary>
        /// Promote
        /// </summary>
        [Text("Promote")]
        Promote,
        /// <summary>
        /// Demote
        /// </summary>
        [Text("Demote")]
        Demote
    }

    /// <summary>
    /// 是否删除
    /// </summary>
    public enum EnumIsDelete
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Text("Normal")]
        Normal = 1,
        /// <summary>
        /// 删除
        /// </summary>
        [Text("Delete")]
        Delete = 0
    }





    /// <summary>
    /// 字段类型
    /// </summary>
    public enum EnumFieldType
    {
        /// <summary>
        /// 单行文本
        /// </summary>
        [Text("SingleLine")]
        SingleLine = 2,
        /// <summary>
        /// 多行文本
        /// </summary>
        [Text("MultiLine")]
        MultiLine = 3,
        /// <summary>
        /// 整形
        /// </summary>
        [Text("Integer")]
        Integer = 0,
        /// <summary>
        /// 浮点型
        /// </summary>
        [Text("Float")]
        Float = 1,
        /// <summary>
        /// Html文本
        /// </summary>
        [Text("Html")]
        Html = 4,
        /// <summary>
        /// 单选项
        /// </summary>
        [Text("Single")]
        Single = 5,
        /// <summary>
        /// 多选项
        /// </summary>
        [Text("Multiple")]
        Multiple = 6,
        /// <summary>
        /// 日期时间
        /// </summary>
        [Text("DateTime")]
        DateTime = 7,
        /// <summary>
        /// 附件
        /// </summary>
        [Text("attachment")]
        Attachment = 8


    }


    /// <summary>
    /// 附件类型
    /// </summary>
    public enum EnumAttachment
    {
        /// <summary>
        /// 图片
        /// </summary>
        [Text("Picture")]
        Picture = 0,
        /// <summary>
        /// 文件
        /// </summary>
        [Text("File")]
        File = 1,
        /// <summary>
        /// 加密文件地址
        /// </summary>
        [Text("EncryptFileUrl")]
        EncryptFileUrl = 2
    }


    /// <summary>
    /// 是否可以搜索
    /// </summary>
    public enum EnumIsSearch
    {
        /// <summary>
        /// 是
        /// </summary>
        [Text("Yes")]
        Yes = 1,
        /// <summary>
        /// 否
        /// </summary>
        [Text("No")]
        No = 0
    }

    /// <summary>
    /// 是否列表显示
    /// </summary>
    public enum EnumIsList
    {
        /// <summary>
        /// 是
        /// </summary>
        [Text("Yes")]
        Yes = 1,
        /// <summary>
        /// 否
        /// </summary>
        [Text("No")]
        No = 0
    }

    /// <summary>
    /// 多选控件类型
    /// </summary>
    public enum EnumListType
    {
        /// <summary>
        /// 复选框
        /// </summary>
        [Text("CheckBox")]
        CheckBox = 0,
        /// <summary>
        /// 多选列表
        /// </summary>
        [Text("ListBox")]
        ListBox = 1,
        /// <summary>
        /// 单选按钮
        /// </summary>
        [Text("RadioButton")]
        RadioButton = 2,
        /// <summary>
        /// 下拉列表
        /// </summary>
        [Text("DropDownList")]
        DropDownList = 3
    }


    /// <summary>
    /// 是否必填
    /// </summary>
    public enum EnumIsEmpty
    {
        /// <summary>
        /// 是
        /// </summary>
        [Text("Yes")]
        Yes = 1,
        /// <summary>
        /// 否
        /// </summary>
        [Text("No")]
        No = 0
    }

    /// <summary>
    /// 列表框类型(0复选框,1多选列表)
    /// </summary>
    public enum EnumMultiType
    {
        /// <summary>
        /// 复选框
        /// </summary>
        [Text("CheckBox")]
        CheckBox = 0,
        /// <summary>
        /// 多选列表
        /// </summary>
        [Text("ListBox")]
        ListBox = 1
    }
    /// <summary>
    /// 单选框类型(0单选按钮,1下拉列表)
    /// </summary>
    public enum EnumSingleType
    {
        /// <summary>
        /// 单选按钮
        /// </summary>
        [Text("RadioButton")]
        RadioButton = 0,
        /// <summary>
        /// 下拉列表
        /// </summary>
        [Text("DropDownList")]
        DropDownList = 1
    }






    /// <summary>
    /// 自定义信息状态
    /// </summary>
    public enum EnumCustomStatus
    {
        /// <summary>
        /// 锁定
        /// </summary>
        [Text("Lock")]
        Lock = 0,
        /// <summary>
        /// 正常
        /// </summary>
        [Text("Normal")]
        Normal = 1,
        /// <summary>
        /// 置顶
        /// </summary>
        [Text("Top")]
        Top = 2,
    }

    /// <summary>
    /// 提示类型
    /// </summary>
    public enum EnumTips
    {
        /// <summary>
        /// Success
        /// </summary>
        [Text("Success")]
        Success = 0,
        /// <summary>
        /// Warning
        /// </summary>
        [Text("Warning")]
        Warning = 1,
        /// <summary>
        /// Error
        /// </summary>
        [Text("Error")]
        Error = 2,
        /// <summary>
        /// Alert
        /// </summary>
        [Text("Alert")]
        Alert = 3
    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public enum EnumMessagesType
    {
        /// <summary>
        /// Success
        /// </summary>
        [Text("dnnFormSuccess")]
        Success = 0,
        /// <summary>
        /// Warning
        /// </summary>
        [Text("dnnFormWarning")]
        Warning = 1,
        /// <summary>
        /// Error
        /// </summary>
        [Text("dnnFormError")]
        Error = 2,
        /// <summary>
        /// Alert
        /// </summary>
        [Text("dnnFormValidationSummary")]
        Alert = 3
    }









    /// <summary>
    /// 文章状态(草稿、正常、锁定、删除)
    /// </summary>
    public enum EnumStatus
    {
        /// <summary>
        /// 草稿
        /// </summary>
        [Text("Draft")]
        Draft = 0,
        /// <summary>
        /// 正常
        /// </summary>
        [Text("Published")]
        Published = 1,
        /// <summary>
        /// 未发布
        /// </summary>
        [Text("Pending")]
        Pending = 2,
        /// <summary>
        /// 锁定
        /// </summary>
        [Text("Locked")]
        Lock = 3,

        /// <summary>
        /// 回收站
        /// </summary>
        [Text("Recycle")]
        Recycle = 4

    }


    /// <summary>
    /// 文章状态作者用
    /// </summary>
    public enum EnumEventStatusByUser
    {
        /// <summary>
        /// 草稿
        /// </summary>
        [Text("Draft")]
        Draft = 0,
        /// <summary>
        /// 发布
        /// </summary>
        [Text("Published")]
        Published = 1,

    }






    /// <summary>
    /// 文章置顶状态(正常、置顶、描红)
    /// </summary>
    public enum EnumEventTopStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Text("Normal")]
        Normal = 0,
        ///// <summary>
        ///// 高亮
        ///// </summary>
        //[Text("Highlight")]
        //Highlight = 1,
        /// <summary>
        /// 置顶
        /// </summary>
        [Text("Top")]
        Top = 2
        ///// <summary>
        ///// 置顶+高亮
        ///// </summary>
        //[Text("Highlight & Top")]
        //HighlightTop = 3




    }



    /// <summary>
    /// 发送订阅通知(0:未发,1:已发)
    /// </summary>
    public enum EnumEventSendSubscribe
    {
        /// <summary>
        /// 已发
        /// </summary>
        [Text("Issued")]
        Issued = 1,
        /// <summary>
        /// 未发
        /// </summary>
        [Text("Not")]
        Not = 0
    }

    /// <summary>
    /// YesNo
    /// </summary>
    public enum EnumYesNo
    {
        /// <summary>
        /// Yes
        /// </summary>
        [Text("Yes")]
        Yes = 1,
        /// <summary>
        /// No
        /// </summary>
        [Text("No")]
        No = 0

    }


    /// <summary>
    /// True False
    /// </summary>
    public enum EnumTrueFalse
    {
        /// <summary>
        /// True
        /// </summary>
        [Text("True")]
        True = 1,
        /// <summary>
        /// False
        /// </summary>
        [Text("False")]
        False = 0

    }

    /// <summary>
    /// 文章榜单
    /// </summary>
    public enum EnumTopList
    {
        /// <summary>
        /// 最新文章
        /// </summary>
        [Text("Latest")]
        Latest = 0,
        /// <summary>
        /// 评论最多
        /// </summary>
        [Text("Hottest Comments")]
        Hottest_Comments = 1,
        /// <summary>
        /// 浏览最多
        /// </summary>
        [Text("Hottest View")]
        Hottest_View = 2

    }

    /// <summary>
    /// RSS文章类型
    /// </summary>
    public enum EnumFeedEvent
    {
        /// <summary>
        /// 摘要
        /// </summary>
        [Text("Summary")]
        Summary = 0,
        /// <summary>
        /// 全文
        /// </summary>
        [Text("Full text")]
        FullText = 1
    }



    /// <summary>
    /// 头像分级
    /// </summary>
    public enum EnumAvatarRating
    {
        /// <summary>
        /// G — Suitable for all audiences
        /// </summary>
        [Text("G — Suitable for all audiences")]
        G = 0,
        /// <summary>
        /// PG — Possibly offensive, usually for audiences 13 and above
        /// </summary>
        [Text("PG — Possibly offensive, usually for audiences 13 and above")]
        PG = 1,
        /// <summary>
        /// R — Intended for adult audiences above 17
        /// </summary>
        [Text("R — Intended for adult audiences above 17")]
        R = 2,
        /// <summary>
        /// X — Even more mature than above
        /// </summary>
        [Text("X — Even more mature than above")]
        X = 3

    }


    /// <summary>
    /// 默认头像
    /// </summary>
    public enum EnumDefaultAvatar
    {
        /// <summary>
        /// mm: (mystery-man) a simple, cartoon-style silhouetted outline of a person (does not vary by email hash)
        /// </summary>
        [Text("mm: (mystery-man) a simple, cartoon-style silhouetted outline of a person (does not vary by email hash)")]
        mm = 0,
        /// <summary>
        /// identicon: a geometric pattern based on an email hash
        /// </summary>
        [Text("identicon: a geometric pattern based on an email hash")]
        identicon = 1,
        /// <summary>
        /// monsterid: a generated 'monster' with different colors, faces, etc
        /// </summary>
        [Text("monsterid: a generated 'monster' with different colors, faces, etc")]
        monsterid = 2,
        /// <summary>
        /// wavatar: generated faces with differing features and backgrounds
        /// </summary>
        [Text("wavatar: generated faces with differing features and backgrounds")]
        wavatar = 3,
        /// <summary>
        /// retro: awesome generated, 8-bit arcade-style pixelated faces
        /// </summary>
        [Text("retro: awesome generated, 8-bit arcade-style pixelated faces")]
        retro = 4
    }

    /// <summary>
    /// 共享工具
    /// </summary>
    public enum EnumSharingTool
    {
        /// <summary>
        /// fb_tw_p1_sc
        /// </summary>
        [Text("fb_tw_p1_sc")]
        fb_tw_p1_sc = 1,
        /// <summary>
        /// large_toolbox
        /// </summary>
        [Text("large_toolbox")]
        large_toolbox = 2,
        /// <summary>
        /// small_toolbox
        /// </summary>
        [Text("small_toolbox")]
        small_toolbox = 3,
        /// <summary>
        /// button
        /// </summary>
        [Text("button")]
        button = 4,
        /// <summary>
        /// 自定义按钮
        /// </summary>
        [Text("Custom Button")]
        CustomButton = 0
    }





    /// <summary>
    /// 控件类型
    /// </summary>
    public enum EnumControlType
    {
        /// <summary>
        /// TextBox
        /// </summary>
        [Text("TextBox")]
        TextBox = 1,
        /// <summary>
        /// RichTextBox
        /// </summary>
        [Text("RichTextBox")]
        RichTextBox = 2,
        /// <summary>
        /// DropDownList
        /// </summary>
        [Text("DropDownList")]
        DropDownList = 3,
        /// <summary>
        /// DropDownList
        /// </summary>
        [Text("DropDownList Group")]
        DropDownList_Group = 30,
        /// <summary>
        /// ListBox
        /// </summary>
        [Text("ListBox")]
        ListBox = 4,
        /// <summary>
        /// ListTree Categorys
        /// </summary>
        [Text("List Tree")]
        ListTree_Categorys = 41,
        /// <summary>
        /// RadioButtonList
        /// </summary>
        [Text("RadioButtonList")]
        RadioButtonList = 5,
        /// <summary>
        /// FileUpload
        /// </summary>
        [Text("FileUpload")]
        FileUpload = 6,
        /// <summary>
        /// Urls
        /// </summary>
        [Text("Urls")]
        Urls = 60,
        /// <summary>
        /// Images
        /// </summary>
        [Text("Images")]
        Images = 61,
        /// <summary>
        /// CheckBox
        /// </summary>
        [Text("CheckBox")]
        CheckBox = 7,
        /// <summary>
        /// CheckBoxList
        /// </summary>
        [Text("CheckBoxList")]
        CheckBoxList = 8,
        /// <summary>
        /// DatePicker
        /// </summary>
        [Text("DatePicker")]
        DatePicker = 9,
        /// <summary>
        /// ColorPicker
        /// </summary>
        [Text("ColorPicker")]
        ColorPicker = 90,
        /// <summary>
        /// IconPicker
        /// </summary>
        [Text("IconPicker")]
        IconPicker = 91,
        /// <summary>
        /// Label
        /// </summary>
        [Text("Label")]
        Label = 10



    }


    /// <summary>
    /// 字段控件类型
    /// </summary>
    public enum EnumFieldControlType
    {
        /// <summary>
        /// TextBox
        /// </summary>
        [Text("TextBox")]
        TextBox = 1,
        /// <summary>
        /// RichTextBox
        /// </summary>
        [Text("RichTextBox")]
        RichTextBox = 2,
        /// <summary>
        /// DropDownList
        /// </summary>
        [Text("DropDownList")]
        DropDownList = 3,
        /// <summary>
        /// ListBox
        /// </summary>
        [Text("ListBox")]
        ListBox = 4,
        /// <summary>
        /// RadioButtonList
        /// </summary>
        [Text("RadioButtonList")]
        RadioButtonList = 5,
        /// <summary>
        /// FileUpload
        /// </summary>
        [Text("FileUpload")]
        FileUpload = 6,
        /// <summary>
        /// Urls
        /// </summary>
        [Text("Urls")]
        Urls = 60,
        /// <summary>
        /// CheckBox
        /// </summary>
        [Text("CheckBox")]
        CheckBox = 7,
        /// <summary>
        /// CheckBoxList
        /// </summary>
        [Text("CheckBoxList")]
        CheckBoxList = 8,
        /// <summary>
        /// DatePicker
        /// </summary>
        [Text("DatePicker")]
        DatePicker = 9,
        /// <summary>
        /// Label
        /// </summary>
        [Text("Label")]
        Label = 10
    }



    /// <summary>
    /// 字段名枚举
    /// </summary>
    public enum EnumFieldName
    {
        /// <summary>
        /// Attribute
        /// </summary>
        [Text("Attribute 1")]
        Attribute01 = 1,
        /// <summary>
        /// Attribute
        /// </summary>
        [Text("Attribute 2")]
        Attribute02 = 2,
        /// <summary>
        /// Attribute
        /// </summary>
        [Text("Attribute 3")]
        Attribute03 = 3,
        /// <summary>
        /// Attribute
        /// </summary>
        [Text("Attribute 4")]
        Attribute04 = 4,
        /// <summary>
        /// Attribute
        /// </summary>
        [Text("Attribute 5")]
        Attribute05 = 5,
        /// <summary>
        /// Attribute
        /// </summary>
        [Text("Attribute 6")]
        Attribute06 = 6,
        /// <summary>
        /// Attribute
        /// </summary>
        [Text("Attribute 7")]
        Attribute07 = 7,
        /// <summary>
        /// Attribute
        /// </summary>
        [Text("Attribute 8")]
        Attribute08 = 8,
        /// <summary>
        /// Attribute
        /// </summary>
        [Text("Attribute 9")]
        Attribute09 = 9

    }
        


    /// <summary>
    /// 列表控件方向
    /// </summary>
    public enum EnumControlDirection
    {

        /// <summary>
        /// 横向
        /// </summary>
        [Text("Horizontal")]
        Horizontal = 1,

        /// <summary>
        /// 垂直
        /// </summary>
        [Text("Vertical")]
        Vertical = 0
    }




    /// <summary>
    /// 文章排序方式
    /// </summary>
    public enum EnumEventSort
    {

        /// <summary>
        /// 默认排序
        /// </summary>
        [Text("Default")]
        Default = 0,
        /// <summary>
        /// 最新的文章
        /// </summary>
        [Text("Latest")]
        Latest = 1,
        /// <summary>
        /// 评论最多的文章
        /// </summary>
        [Text("Hottest Comments")]
        Hottest_Comments = 2,
        /// <summary>
        /// 浏览次数最多的文章
        /// </summary>
        [Text("Hottest View")]
        Hottest_View = 3

    }






    /// <summary>
    /// 验证类型
    /// </summary>
    public enum EnumVerification
    {
        /// <summary>
        /// 表示可选项。若不输入，不要求必填，若有输入，则验证其是否符合要求。
        /// </summary>
        [Text("optional")]
        optional = 0,
        /// <summary>
        /// 验证整数
        /// </summary>
        [Text("integer")]
        integer = 1,
        /// <summary>
        /// 验证数字
        /// </summary>
        [Text("number")]
        number = 2,
        /// <summary>
        /// 验证日期，格式为 YYYY/MM/DD、YYYY/M/D、YYYY-MM-DD、YYYY-M-D
        /// </summary>
        [Text("dateFormat")]
        date = 3,
        /// <summary>
        /// 验证 Email 地址
        /// </summary>
        [Text("email")]
        email = 4,
        /// <summary>
        /// 验证电话号码
        /// </summary>
        [Text("phone")]
        phone = 5,
        /// <summary>
        /// 验证 ipv4 地址
        /// </summary>
        [Text("ipv4")]
        ipv4 = 6,
        /// <summary>
        /// 验证 url 地址，需以 http://、https:// 或 ftp:// 开头
        /// </summary>
        [Text("url")]
        url = 7,
        /// <summary>
        /// 只接受填数字和空格
        /// </summary>
        [Text("onlyNumberSp")]
        onlyNumberSp = 8,
        /// <summary>
        /// 只接受填英文字母（大小写）和单引号(')
        /// </summary>
        [Text("onlyLetterSp")]
        onlyLetterSp = 9,
        /// <summary>
        /// 只接受数字和英文字母
        /// </summary>
        [Text("onlyLetterNumber")]
        onlyLetterNumber = 10



    }


    /// <summary>
    /// 特性模式
    /// </summary>
    public enum EnumFeature
    {
        /// <summary>
        /// 默认
        /// </summary>
        [Text("Default")]
        Default = 0,
        /// <summary>
        /// 开启
        /// </summary>
        [Text("Yes")]
        Yes = 1,
        /// <summary>
        /// 不开启
        /// </summary>
        [Text("No")]
        No = 2
    }
    /// <summary>
    /// 用这个标签告诉Google此链接可能会出现的更新频率
    /// "always", "hourly", "daily", "weekly", "monthly", "yearly"
    /// </summary>
    public enum EnumSitemapChangefreq
    {
        /// <summary>
        /// always
        /// </summary>
        [Text("always")]
        always = 1,
        /// <summary>
        /// hourly
        /// </summary>
        [Text("hourly")]
        hourly = 2,
        /// <summary>
        /// daily
        /// </summary>
        [Text("daily")]
        daily = 3,
        /// <summary>
        /// weekly
        /// </summary>
        [Text("weekly")]
        weekly = 4,
        /// <summary>
        /// monthly
        /// </summary>
        [Text("monthly")]
        monthly = 5,
        /// <summary>
        /// yearly
        /// </summary>
        [Text("yearly")]
        yearly = 6
    }

    /// <summary>
    /// 订阅状态
    /// </summary>
    public enum EnumSubscriptionStatus
    {
        /// <summary>
        /// 锁定
        /// </summary>
        [Text("Lock")]
        Lock = 0,
        /// <summary>
        /// 正常
        /// </summary>
        [Text("Approved")]
        Approved = 1,
        /// <summary>
        /// 回收站
        /// </summary>
        [Text("Recycle")]
        Recycle = 2

    }

    /// <summary>
    /// 邮件订阅类型
    /// </summary>
    public enum EnumSubscriptionType
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        [Text("Email")]
        Email = 0,
        /// <summary>
        /// 用户
        /// </summary>
        [Text("User")]
        User = 1,
        /// <summary>
        /// 角色
        /// </summary>
        [Text("Role")]
        Role = 2,
        /// <summary>
        /// 退订
        /// </summary>
        [Text("Unsubscribe")]
        Unsubscribe = 9,

    }

    /// <summary>
    /// 邮件的认证方式
    /// </summary>
    public enum EnumEmailAuthentication
    {
        /// <summary>
        /// Anonymous
        /// </summary>
        [Text("Anonymous")]
        Anonymous = 0,
        /// <summary>
        /// Basic
        /// </summary>
        [Text("Basic")]
        Basic = 1,
        /// <summary>
        /// NTLM
        /// </summary>
        [Text("NTLM")]
        NTLM = 2

    }


    /// <summary>
    /// 文件类型
    /// </summary>
    public enum EnumFileMate
    {
        /// <summary>
        /// Image
        /// </summary>
        [Text("Image")]
        Image = 0,
        /// <summary>
        /// Video
        /// </summary>
        [Text("Video")]
        Video = 1,
        /// <summary>
        /// Audio
        /// </summary>
        [Text("Audio")]
        Audio = 2,
        /// <summary>
        /// Zip
        /// </summary>
        [Text("Zip")]
        Zip = 3,
        /// <summary>
        /// Doc
        /// </summary>
        [Text("Doc")]
        Doc = 4,
        /// <summary>
        /// Other
        /// </summary>
        [Text("Other")]
        Other = 9,



    }

    /// <summary>
    /// 多媒体文件状态(未审核、正常、删除/回收站)
    /// </summary>
    public enum EnumFileStatus
    {
        /// <summary>
        /// 未处理
        /// </summary>
        [Text("Pending")]
        Pending = 0,

        /// <summary>
        /// 正常
        /// </summary>
        [Text("Approved")]
        Approved = 1,
        /// <summary>
        /// 回收站
        /// </summary>
        [Text("Recycle")]
        Recycle = 2
    }

    /// <summary>
    /// 文章文件关系
    /// </summary>
    public enum EnumEventFileType
    {
        /// <summary>
        /// 图片
        /// </summary>
        [Text("Image")]
        Image = 0,

        /// <summary>
        /// 附件
        /// </summary>
        [Text("Attachment")]
        Attachment = 1
    }

    /// <summary>
    /// 已存在文章(采集)
    /// </summary>
    public enum EnumAlreadyExists
    {
        /// <summary>
        /// 忽略
        /// </summary>
        [Text("Ignore")]
        Ignore = 0,

        /// <summary>
        /// 覆盖
        /// </summary>
        [Text("Covering")]
        Covering = 1,

        /// <summary>
        /// 新建
        /// </summary>
        [Text("New")]
        New = 2

    }

 


    /// <summary>
    /// 链接控件枚举
    /// </summary>
    public enum EnumUrlControls
    {
        /// <summary>
        /// URL
        /// </summary>
        [Text("URL ( A Link To An External Resource )")]
        Url = 1,
        /// <summary>
        /// 页面
        /// </summary>
        [Text("Page ( A Page On Your Site )")]
        Page = 2,
        /// <summary>
        /// 文件
        /// </summary>
        [Text("Files ( From the media library )")]
        Files = 3

    }

    /// <summary>
    /// 头像类型
    /// </summary>
    public enum EnumAvatarType
    {
        /// <summary>
        /// Gravatar
        /// </summary>
        [Text("Gravatar ( gravatar.com )")]
        Gravatar = 0,
        /// <summary>
        /// UserProfile
        /// </summary>
        [Text("UserProfile ( DNN )")]
        UserProfile = 1,
        /// <summary>
        /// GravatarUserProfile
        /// </summary>
        [Text("UserProfile & Gravatar")]
        GravatarUserProfile = 2

    }

    /// <summary>
    /// 评论服务
    /// </summary>
    public enum EnumCommentService
    {
        [Text("Normal")]
        Normal = 0,

        [Text("Disqus")]
        Disqus = 1
    }

    /// <summary>
    /// 模块属性(仪表盘/通用/列表/详情)
    /// </summary>
    public enum EnumTemplateAttribute
    {
        /// <summary>
        /// 仪表盘
        /// </summary>
        [Text("DashBoard")]
        DashBoard = 0,
        /// <summary>
        /// 通用DashBoard和List
        /// </summary>
        [Text("DashBoard&List")]
        DashBoard_List = 1,
        /// <summary>
        /// 列表
        /// </summary>
        [Text("List")]
        List = 2,
        /// <summary>
        /// 详情
        /// </summary>
        [Text("Detail")]
        Detail = 3
    }


    /// <summary>
    /// 月份枚举
    /// </summary>
    public enum EnumMonth
    {
        /// <summary>
        /// 1月
        /// </summary>
        [Text("January")]
        January = 1,
        /// <summary>
        /// 2月
        /// </summary>
        [Text("February")]
        February = 2,
        /// <summary>
        /// 3月
        /// </summary>
        [Text("March")]
        March = 3,
        /// <summary>
        /// 4月
        /// </summary>
        [Text("April")]
        April = 4,
        /// <summary>
        /// 5月
        /// </summary>
        [Text("May")]
        May = 5,
        /// <summary>
        /// 6月
        /// </summary>
        [Text("June")]
        June = 6,
        /// <summary>
        /// 7月
        /// </summary>
        [Text("July")]
        July = 7,
        /// <summary>
        /// 8月
        /// </summary>
        [Text("August")]
        Aguest = 8,
        /// <summary>
        /// 9月
        /// </summary>
        [Text("September")]
        September = 9,
        /// <summary>
        /// 10月
        /// </summary>
        [Text("October")]
        October = 10,
        /// <summary>
        /// 11月
        /// </summary>
        [Text("November")]
        November = 11,
        /// <summary>
        /// 12月
        /// </summary>
        [Text("December")]
        December = 12
    }


    /// <summary>
    /// 月份枚举
    /// </summary>
    public enum EnumMonthSimple
    {
        /// <summary>
        /// 1月
        /// </summary>
        [Text("jan")]
        jan = 1,
        /// <summary>
        /// 2月
        /// </summary>
        [Text("feb")]
        feb = 2,
        /// <summary>
        /// 3月
        /// </summary>
        [Text("mar")]
        mar = 3,
        /// <summary>
        /// 4月
        /// </summary>
        [Text("apr")]
        apr = 4,
        /// <summary>
        /// 5月
        /// </summary>
        [Text("may")]
        may = 5,
        /// <summary>
        /// 6月
        /// </summary>
        [Text("jun")]
        jun = 6,
        /// <summary>
        /// 7月
        /// </summary>
        [Text("jul")]
        jul = 7,
        /// <summary>
        /// 8月
        /// </summary>
        [Text("aug")]
        aug = 8,
        /// <summary>
        /// 9月
        /// </summary>
        [Text("sep")]
        sep = 9,
        /// <summary>
        /// 10月
        /// </summary>
        [Text("oct")]
        oct = 10,
        /// <summary>
        /// 11月
        /// </summary>
        [Text("nov")]
        nov = 11,
        /// <summary>
        /// 12月
        /// </summary>
        [Text("dec")]
        dec = 12
    }


























    /// <summary>
    /// 星期枚举
    /// </summary>
    public enum EnumWeek
    {
        /// <summary>
        /// Monday
        /// </summary>
        [Text("Monday")]
        Monday = 1,
        /// <summary>
        /// Tuesday
        /// </summary>
        [Text("Tuesday")]
        Tuesday = 2,
        /// <summary>
        /// Wednesday
        /// </summary>
        [Text("Wednesday")]
        Wednesday = 3,
        /// <summary>
        /// Thursday
        /// </summary>
        [Text("Thursday")]
        Thursday = 4,
        /// <summary>
        /// Friday
        /// </summary>
        [Text("Friday")]
        Friday = 5,
        /// <summary>
        /// Saturday
        /// </summary>
        [Text("Saturday")]
        Saturday = 6,
        /// <summary>
        /// Sunday
        /// </summary>
        [Text("Sunday")]
        Sunday = 0

    }

    /// <summary>
    /// 星期枚举
    /// </summary>
    public enum EnumWeekSimple
    {
        /// <summary>
        /// Sunday
        /// </summary>
        [Text("sun")]
        sun = 0,
        /// <summary>
        /// Monday
        /// </summary>
        [Text("mon")]
        mon = 1,
        /// <summary>
        /// Tuesday
        /// </summary>
        [Text("tue")]
        tue = 2,
        /// <summary>
        /// Wednesday
        /// </summary>
        [Text("wed")]
        wed = 3,
        /// <summary>
        /// Thursday
        /// </summary>
        [Text("thu")]
        thu = 4,
        /// <summary>
        /// Friday
        /// </summary>
        [Text("fri")]
        fri = 5,
        /// <summary>
        /// Saturday
        /// </summary>
        [Text("sat")]
        sat = 6


    }



    /// <summary>
    /// 一月中的第几周枚举
    /// </summary>
    public enum EnumFollowWeekOfMonth
    {
        /// <summary>
        /// 1st
        /// </summary>
        [Text("1st")]
        st1 = 1,
        /// <summary>
        /// 2nd
        /// </summary>
        [Text("2nd")]
        nd2 = 2,
        /// <summary>
        /// 3rd
        /// </summary>
        [Text("3rd")]
        rd3 = 3,
        /// <summary>
        /// 4th
        /// </summary>
        [Text("4th")]
        th4 = 4,
        /// <summary>
        /// 5th
        /// </summary>
        [Text("5th")]
        th5 = 5,
        /// <summary>
        /// last
        /// </summary>
        [Text("last")]
        last = 0


    }


    





     

/// <summary>
/// 宽度后缀(px / %)
/// </summary>
public enum EnumWidthSuffix
    {
        /// <summary>
        /// 像素
        /// </summary>
        [Text("px")]
        px = 0,
        /// <summary>
        /// 百分比
        /// </summary>
        [Text("%")]
        Percentage = 1
    }

    /// <summary>
    /// URL控件的选择的枚举
    /// </summary>
    public enum EnumUrls
    { 
        /// <summary>
        /// 页面
        /// </summary>
        [Text("Page")]
        Page = 0,
        /// <summary>
        /// 文件选择
        /// </summary>
        [Text("File")]
        File = 1,
        /// <summary>
        /// 链接
        /// </summary>
        [Text("Url")]
        Url = 2



    }

     

    /// <summary>
    /// Product Roadmap 首页需要选择的数据类型
    /// </summary>
    public enum EnumIncludedSections
    {
        /// <summary>
        /// GameSheets
        /// </summary>
        [Text("Game Sheets")]
        GameSheets = 0,
        /// <summary>
        /// Downloads
        /// </summary>
        [Text("Downloads")]
        Downloads = 1,
        /// <summary>
        /// Campaigns
        /// </summary>
        [Text("Campaigns")]
        Campaigns = 2,
        /// <summary>
        /// Events
        /// </summary>
        [Text("Events")]
        Events = 3
    }

    /// <summary>
    /// 提醒状态
    /// </summary>
    public enum EnumNotificationStatus
    {
        /// <summary>
        /// 无状态
        /// </summary>
        [Text("None")]
        None = 0,
        /// <summary>
        /// GameSheets
        /// </summary>
        [Text("New")]
        New = 1,
        /// <summary>
        /// Downloads
        /// </summary>
        [Text("Update")]
        Update = 2,
    }


    /// <summary>
    /// 动态模块的类型
    /// </summary>
    public enum EnumDynamicModuleType
    {
        /// <summary>
        /// Product Roadmap
        /// </summary>
        [Text("Product Roadmap")]
        ProductRoadmap = 0,
        /// <summary>
        /// Help
        /// </summary>
        [Text("Help")]
        Help = 1,
        /// <summary>
        /// Game Sheet
        /// </summary>
        [Text("Game Sheet")]
        GameSheet =2,
        /// <summary>
        /// Campaign
        /// </summary>
        [Text("Campaign")]
        Campaign = 3


    }


    /// <summary>
    /// 动态模块项的类型
    /// </summary>
    public enum EnumDynamicItemType
    {
        /// <summary>
        /// Text
        /// </summary>
        [Text("Text")]
        Text = 0,
        /// <summary>
        /// Image
        /// </summary>
        [Text("Image")]
        Image = 1,
        /// <summary>
        /// Image Text
        /// </summary>
        [Text("Image Text")]
        ImageText = 2,
        /// <summary>
        /// Video
        /// </summary>
        [Text("Video")]
        Video = 3,
        /// <summary>
        /// xFrame
        /// </summary>
        [Text("iFrame")]
        xFrame = 4 


    }




    /// <summary>
    /// 显示模块类型
    /// </summary>
    public enum EnumDisplayModuleType
    {
        /// <summary>
        /// Product Roadmap
        /// </summary>
        [Text("Product Roadmap")]
        ProductRoadmap = 0,
        /// <summary>
        /// Game Sheet
        /// </summary>
        [Text("Game Sheets")]
        GameSheets = 1,
        /// <summary>
        /// Downloads
        /// </summary>
        [Text("Downloads")]
        Downloads = 2,
        /// <summary>
        /// Campaigns
        /// </summary>
        [Text("Campaigns")]
        Campaigns = 3,
        /// <summary>
        /// Events
        /// </summary>
        [Text("Events")]
        Events = 4,
        /// <summary>
        /// Game Sheet
        /// </summary>
        [Text("My Account")]
        MyAccount = 5,
        /// <summary>
        /// Help
        /// </summary>
        [Text("Help")]
        Help = 6


    }



    public enum EnumSortQueryByGame
    {
        /// <summary>
        /// Release Date [New - Old]
        /// </summary>
        [Text("Release Date [New - Old]")]
        ReleaseDate_DESC = 1,
        /// <summary>
        /// Release Date [Old - New]
        /// </summary>
        [Text("Release Date [Old - New]")]
        ReleaseDate_ASC = 2,
        /// <summary>
        /// Release Date [New - Old]
        /// </summary>
        [Text("Game Name [A - Z]")]
        GameName_ASC = 3,
        /// <summary>
        /// Release Date [Old - New]
        /// </summary>
        [Text("Game Name [Z - A]")]
        GameName_DESC = 4,
        /// <summary>
        /// Release Date [New - Old]
        /// </summary>
        [Text("Game ID Desktop [High- Low]")]
        GameID_ASC = 5,
        /// <summary>
        /// Release Date [Old - New]
        /// </summary>
        [Text("Game ID Desktop [Low - High]")]
        GameID_DESC = 6
    }



    public enum EnumSortQueryByGame2
    {
        /// <summary>
        /// Release Date [New - Old]
        /// </summary>
        [Text("Release Date [New - Old]")]
        ReleaseDate_DESC = 1,
        /// <summary>
        /// Release Date [Old - New]
        /// </summary>
        [Text("Release Date [Old - New]")]
        ReleaseDate_ASC = 2,
        /// <summary>
        /// Release Date [New - Old]
        /// </summary>
        [Text("Game Name [A - Z]")]
        GameName_ASC = 3,
        /// <summary>
        /// Release Date [Old - New]
        /// </summary>
        [Text("Game Name [Z - A]")]
        GameName_DESC = 4,
        /// <summary>
        /// Release Date [New - Old]
        /// </summary>
        [Text("Document ID [High- Low]")]
        GameID_ASC = 5,
        /// <summary>
        /// Release Date [Old - New]
        /// </summary>
        [Text("Document ID [Low - High]")]
        GameID_DESC = 6
    }


    public enum EnumSortQueryByEvent
    {
        /// <summary>
        /// Events Date [New - Old]
        /// </summary>
        [Text("Events Date [New - Old]")]
        EventDate_DESC = 1,
        /// <summary>
        /// Events Date [Old - New]
        /// </summary>
        [Text("Events Date [Old - New]")]
        EventDate_ASC = 2,
        /// <summary>
        /// Events Name [A - Z]
        /// </summary>
        [Text("Events Name [A - Z]")]
        EventName_ASC = 3,
        /// <summary>
        /// Events Name [Z - A]
        /// </summary>
        [Text("Events Name [Z - A]")]
        EventName_DESC = 4
    }




}