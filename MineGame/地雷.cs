﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineGame
{
    public enum 区块状态 { 未知, 标记, 安全, 爆炸, 可疑 }
    class 区块 : Button
    {
        int _在雷场中的行位置;
        public int 在雷场中的行位置
        {
            get { return _在雷场中的行位置; }
            set { _在雷场中的行位置 = value; }
        }
        int _在雷场中的列位置;
        public int 在雷场中的列位置
        {
            get { return _在雷场中的列位置; }
            set { _在雷场中的列位置 = value; }
        }
        static int _地雷宽度 = 35;
        public static int 高度
        {
            get { return 区块._地雷宽度; }
        }
        static int _宽 = 35;
        public static int 长度
        {
            get { return 区块._宽; }
        }
        Boolean _is地雷;
        public Boolean Is地雷
        {
            get { return _is地雷; }
        }
        static 雷场 _所属雷场;
        public static 雷场 所属雷场
        {
            get { return 区块._所属雷场; }
        }

        static Boolean _游戏是否结束 = false;
        public static Boolean 游戏是否结束
        {
            get { return 区块._游戏是否结束; }
            private set { 区块._游戏是否结束 = value; }
        }

        int _已标记的周围邻居数量=0;


        static int _已打开安全区数量 = 0;
        private int 已打开安全区数量
        {
            set
            {
                _已打开安全区数量++;
                if (_所属雷场.地雷总数 + _已打开安全区数量 == _所属雷场.雷场总行数 * _所属雷场.雷场总列数)
                    游戏结束(true);
            }
        }
        区块状态 _当前状态;
        public 区块状态 当前状态
        {
            get {
                return _当前状态;
            }
            set
            {
                _当前状态 = value;

                if (_当前状态 == 区块状态.爆炸)
                    游戏结束(false);
                else
                {
                    if (_当前状态 == 区块状态.安全)
                    {
                        已打开安全区数量 = 1;
                        //快速开辟安全区
                        快速开辟安全区(_在雷场中的行位置, _在雷场中的列位置);
                    }

                    修改区块当前外观(value);
                }
            }
        }
        void 快速开辟安全区(int 在雷场中的行位置, int 在雷场中的列位置)
        {
            if (_已标记的周围邻居数量 == _所属雷场.地雷分布状态[在雷场中的行位置, 在雷场中的列位置])
            {
                foreach (Object control in _所属雷场.Controls)
                {
                    try
                    {
                        区块 _区块 = (区块)control;

                        if (_区块.当前状态 == 区块状态.未知 || _区块.当前状态 == 区块状态.可疑)
                        {

                            #region
                            try
                            {
                                //左上角
                                if (_区块.在雷场中的行位置 - 1 == _在雷场中的行位置 &&
                                    _区块.在雷场中的列位置 - 1 == _在雷场中的列位置)
                                    _区块.当前状态 = _区块._is地雷 ? 区块状态.爆炸 : 区块状态.安全;
                            }
                            catch (Exception) {  /*坐标越界*/ }
                            try
                            {
                                //正左边
                                if (_区块.在雷场中的行位置 == _在雷场中的行位置 &&
                                    _区块.在雷场中的列位置 - 1 == _在雷场中的列位置)
                                    _区块.当前状态 = _区块._is地雷 ? 区块状态.爆炸 : 区块状态.安全;
                            }
                            catch (Exception) {  /*坐标越界*/ }
                            try
                            {
                                //左下角
                                if (_区块.在雷场中的行位置 + 1 == _在雷场中的行位置 &&
                                    _区块.在雷场中的列位置 - 1 == _在雷场中的列位置)
                                    _区块.当前状态 = _区块._is地雷 ? 区块状态.爆炸 : 区块状态.安全;
                            }
                            catch (Exception) {  /*坐标越界*/ }
                            try
                            {
                                //正上方
                                if (_区块.在雷场中的行位置 - 1 == _在雷场中的行位置 &&
                                    _区块.在雷场中的列位置 == _在雷场中的列位置)
                                    _区块.当前状态 = _区块._is地雷 ? 区块状态.爆炸 : 区块状态.安全;
                            }
                            catch (Exception) {  /*坐标越界*/ }
                            try
                            {
                                //正下方
                                if (_区块.在雷场中的行位置 + 1 == _在雷场中的行位置 &&
                                    _区块.在雷场中的列位置 == _在雷场中的列位置)
                                    _区块.当前状态 = _区块._is地雷 ? 区块状态.爆炸 : 区块状态.安全;
                            }
                            catch (Exception) {  /*坐标越界*/ }
                            try
                            {
                                //右上角
                                if (_区块.在雷场中的行位置 - 1 == _在雷场中的行位置 &&
                                    _区块.在雷场中的列位置 + 1 == _在雷场中的列位置)
                                    _区块.当前状态 = _区块._is地雷 ? 区块状态.爆炸 : 区块状态.安全;
                            }
                            catch (Exception) {  /*坐标越界*/ }
                            try
                            {
                                //正右边
                                if (_区块.在雷场中的行位置 == _在雷场中的行位置 &&
                                    _区块.在雷场中的列位置 + 1 == _在雷场中的列位置)
                                    _区块.当前状态 = _区块._is地雷 ? 区块状态.爆炸 : 区块状态.安全;
                            }
                            catch (Exception) {  /*坐标越界*/ }
                            try
                            {
                                //右下角
                                if (_区块.在雷场中的行位置 + 1 == _在雷场中的行位置 &&
                                    _区块.在雷场中的列位置 + 1 == _在雷场中的列位置)
                                    _区块.当前状态 = _区块._is地雷 ? 区块状态.爆炸 : 区块状态.安全;
                            }
                            catch (Exception) {  /*坐标越界*/ }
                            #endregion
                        }
                    }
                    catch (Exception) { ;}
                }
            }
        }

        public 区块(Boolean 是否是地雷, 雷场 雷场)
        {            
            初始化地雷(是否是地雷, 雷场);
        }
        void 初始化地雷(Boolean 是否是地雷, 雷场 雷场)
        {
            区块._所属雷场 = 雷场;
            _is地雷 = 是否是地雷;
            this.MouseDown += 处理鼠标被点击的方法;
            this.Size = new System.Drawing.Size(_地雷宽度, _宽);
            当前状态 = 区块状态.未知;
        }              
        
        void 处理鼠标被点击的方法(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (游戏是否结束)
            {
                return;
            }
            //改变当前方块状态
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    if (_当前状态 == 区块状态.未知)
                        当前状态 = _is地雷 ? 区块状态.爆炸 : 区块状态.安全;
                    else if (_当前状态 == 区块状态.安全)
                    {
                        //快速开辟安全区
                        快速开辟安全区(_在雷场中的行位置, _在雷场中的列位置);
                       
                    }
                    break;

                case System.Windows.Forms.MouseButtons.Right:
                    if (_当前状态 == 区块状态.未知)
                    {
                        当前状态 = 区块状态.标记;
                        修改周围区块的已标记数量(_在雷场中的行位置, _在雷场中的列位置,true);
                    }
                    else if (_当前状态 == 区块状态.标记)
                    {
                        当前状态 = 区块状态.可疑;
                        修改周围区块的已标记数量(_在雷场中的行位置, _在雷场中的列位置, false);
                    }
                    else if (_当前状态 == 区块状态.可疑)
                        当前状态 = 区块状态.未知;
                    break;
                default:
                    break;
            }

        }
        void 修改周围区块的已标记数量(int 在雷场中的行位置,int 在雷场中的列位置,Boolean 是否增)
        {
            foreach (Object control in _所属雷场.Controls)
            {
                try
                {
                    区块 _区块 = (区块)control;

                    if (!_区块._is地雷)
                    {
                        #region
                        try
                        {
                            //左上角
                            if (_区块.在雷场中的行位置 - 1 == 在雷场中的行位置 &&
                                _区块.在雷场中的列位置 - 1 == 在雷场中的列位置)
                                _区块._已标记的周围邻居数量 += 是否增?1:-1;
                        }
                        catch (Exception) {  /*坐标越界*/ }
                        try
                        {
                            //正左边
                            if (_区块.在雷场中的行位置 == 在雷场中的行位置 &&
                                _区块.在雷场中的列位置 - 1 == 在雷场中的列位置)
                                _区块._已标记的周围邻居数量 += 是否增 ? 1 : -1;
                        }
                        catch (Exception) {  /*坐标越界*/ }
                        try
                        {
                            //左下角
                            if (_区块.在雷场中的行位置 + 1 == 在雷场中的行位置 &&
                                _区块.在雷场中的列位置 - 1 == 在雷场中的列位置)
                                _区块._已标记的周围邻居数量 += 是否增 ? 1 : -1;
                        }
                        catch (Exception) {  /*坐标越界*/ }
                        try
                        {
                            //正上方
                            if (_区块.在雷场中的行位置 - 1 == 在雷场中的行位置 &&
                                _区块.在雷场中的列位置 == 在雷场中的列位置)
                                _区块._已标记的周围邻居数量 += 是否增 ? 1 : -1;
                        }
                        catch (Exception) {  /*坐标越界*/ }
                        try
                        {
                            //正下方
                            if (_区块.在雷场中的行位置 + 1 == 在雷场中的行位置 &&
                                _区块.在雷场中的列位置 == 在雷场中的列位置)
                                _区块._已标记的周围邻居数量 += 是否增 ? 1 : -1;
                        }
                        catch (Exception) {  /*坐标越界*/ }
                        try
                        {
                            //右上角
                            if (_区块.在雷场中的行位置 - 1 == 在雷场中的行位置 &&
                                _区块.在雷场中的列位置 + 1 == 在雷场中的列位置)
                                _区块._已标记的周围邻居数量 += 是否增 ? 1 : -1;
                        }
                        catch (Exception) {  /*坐标越界*/ }
                        try
                        {
                            //正右边
                            if (_区块.在雷场中的行位置 == 在雷场中的行位置 &&
                                _区块.在雷场中的列位置 + 1 == 在雷场中的列位置)
                                _区块._已标记的周围邻居数量 += 是否增 ? 1 : -1;
                        }
                        catch (Exception) {  /*坐标越界*/ }
                        try
                        {
                            //右下角
                            if (_区块.在雷场中的行位置 + 1 == 在雷场中的行位置 &&
                                _区块.在雷场中的列位置 + 1 == 在雷场中的列位置)
                                _区块._已标记的周围邻居数量 += 是否增 ? 1 : -1;
                        }
                        catch (Exception) {  /*坐标越界*/ }
                        #endregion
                    }
                }
                catch (Exception) { ;}
            }

        }

        void 修改区块当前外观(区块状态 当前外观)
        {
            System.Drawing.Image img;
            img = null;

            switch (当前外观)
            {
                case 区块状态.未知:
                    img = Properties.Resources.n;
                    break;
                case 区块状态.爆炸:
                    img = Properties.Resources.c;
                    break;
                case 区块状态.标记:
                    img = Properties.Resources.f;
                    break;
                case 区块状态.可疑:
                    img = Properties.Resources.q;
                    break;
                default:
                    img = 设置区块外观();
                    break;
            }

            this.Image = img;
        }

        System.Drawing.Image 设置区块外观()
        {
            System.Drawing.Image img;
            switch (_所属雷场.地雷分布状态[_在雷场中的行位置, _在雷场中的列位置])
            {
                case 0:
                    img = Properties.Resources._0;
                    break;
                case 1:
                    img = Properties.Resources._1;
                    break;
                case 2:
                    img = Properties.Resources._2;
                    break;
                case 3:
                    img = Properties.Resources._3;
                    break;
                case 4:
                    img = Properties.Resources._4;
                    break;
                case 5:
                    img = Properties.Resources._5;
                    break;
                case 6:
                    img = Properties.Resources._6;
                    break;
                case 7:
                    img = Properties.Resources._7;
                    break;
                case 8:
                    img = Properties.Resources._8;
                    break;
                default:
                    img = Properties.Resources.b;
                    break;
            }
            return img;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="胜利"></param>
        void 游戏结束(Boolean 胜利)
        {
            游戏是否结束 = true;
            if (!胜利)
            {
                引爆所有地雷();
                MessageBox.Show("很遗憾，你失败了！");
            }else
            MessageBox.Show("恭喜你，你成功了！");
        }
        void 引爆所有地雷()
        {
            foreach (Object control in _所属雷场.Controls)
            {
                try
                {
                    区块 _地雷 = (区块)control;
                    if (_地雷 != this)
                        _地雷.Image = _地雷.设置区块外观();
                    else
                        _地雷.Image = Properties.Resources.c;
                }
                catch (Exception) { ;}
            }
        }
    }


}
