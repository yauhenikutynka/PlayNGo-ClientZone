using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Playngo.Modules.ClientZone
{
    /// <summary>
    /// 初始化类
    /// </summary>
    public class Initialization
    {

        private BaseModule _bpm = new BaseModule();
        /// <summary>
        /// 模块基类
        /// </summary>
        public BaseModule Bpm
        {
            get { return _bpm; }
            set { _bpm = value; }
        }

        /// <summary>
        /// 博客初始化标志
        /// </summary>
        public Boolean ClientZone_Init
        {
            get { return Bpm.Settings["ClientZone_Init"] != null && !string.IsNullOrEmpty(Bpm.Settings["ClientZone_Init"].ToString()) ? Convert.ToBoolean(Bpm.Settings["ClientZone_Init"]) : false; }
        }


        /// <summary>
        /// 构造类
        /// </summary>
        /// <param name="bpm"></param>
        public Initialization(BaseModule __bpm)
        {
            Bpm = __bpm;
        }

        /// <summary>
        /// 初始化的方法
        /// </summary>
        public void Init()
        {
            if (!ClientZone_Init)
            {
                QueryParam qp = new QueryParam();
                qp.Where.Add(new SearchParam(Playngo_ClientZone_Event._.ModuleId, Bpm.ModuleId, SearchType.Equal));
                if (Playngo_ClientZone_Event.FindCount(qp) == 0)//文章数为0的情况下，才增加默认的文章和分类
                {
                    //初始化分类和文章
                    //Int32 EventID = Playngo_ClientZone_Event.Initialization(Bpm);

           
                }

                Bpm.UpdateModuleSetting("ClientZone_Init", "true");
            }
        }




    }
}