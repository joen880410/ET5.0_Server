# Jacfit-開發篇

## 1.開發環境
* 作業系統
    * Windows 10/7 64 bit
* 開發工具
    * IDE(任選一種)
      * Visual Studio 2017/2019(請更新到最新)
          * .net 桌面開發
          * 下載 .NET 6(for vs2017/2019版)
          * 安裝visual studio tools for unity(Debug用)
          * 在Unity有相應的`Package`需要安裝
      * JetBrains Rider 2021.3.3(推薦)
          * .NET 6
          * 在Unity有相應的Package需要安裝
    * Unity2020.3.20f1
        * 要輸出Android app請再安裝Android平台的SDK(請直接安裝Android Studio)
        * 該版本Unity解決了Apple Store審查UIWebView的問題(可以在Mac的XCode專案目錄下使用$"grep -r UIWebView . "查看)
* 其他軟體
    * MongoDB 用最新版 
        * port不用特別設(預設27027)
    * MongoDB GUI(資料庫圖形化界面)
      * MongoDB Compass 用最新版
      * (推薦)3T Studio(版本自選)
    * Windows Redis 用最新版
    * P3X Redis UI 用最新版(Redis圖形化介面)
    * pm2(進程管理工具)
       * 安裝pm2前請先安裝nodejs(版本不限)
       * 執行命令$"npm install pm2 -g"進行安裝(版本不限)
## 2.運行
* 1.在資料夾Config下找到`LocalAllServer.txt`，修改裡面的各個服務器Ip:Port設定
* 2.用IDE開啟檔案`Server.sln`，在`方案總管`對根目錄的`解決方案`右鍵`重建方案`，一次編譯好
* 3.在IDE直接執行
## 3.運行分布式
 * 1.在根目錄下運行`Server_Build.bat`，並選擇相應平台進行編譯
 * 2.在資料夾Config下找到`Release.txt`，並調整對應的IP跟端口
 * 3.再執行`apps_win_all.json`會使用pm2開啟分布式Server
## 4.運行壓力測試
 * 1.在根目錄下運行`Server_Build.bat`，並選擇相應平台進行編譯
 * 2.在資料夾Config下找到`Benchmark.txt`，並調整對應的IP跟端口
   * 如果你本地端的`Benchmark.txt`過舊，可以參考`Benchmark_Default.txt`
 * 3.再執行`Server_Start_Win_Robot.bat`會使用cmd開啟壓力測試程式

# Jacfit-監測篇
 
## 1.安裝工具&運行
 * Prometheus(請用最新版)
   * 請根據相應平台進行安裝
   * 特性
     * 能任意自訂較為細部的監測目標(如上線人數)
     * 系統監控警報框架
     * 時間序列資料庫`TSDB`
     * 靈活的查詢語言(`PromQL`)，如可對監測數據進行加減乘除等
     * 基於HTTP的Pull方式收集時序資料(預設15秒Pull一次)
     * 支援多種視覺化儀表板呈現，如`Grafana`
     * 開源且免費
     * 專案有多人在維護，有BUG容易得到解決
     * 更多特性請查閱參考連結
   * 安裝完成後，請配置`prometheus.yml`文件並起動`Prometheus`
     * `prometheus.yml`文件主要是配置需要拉取資料的目標Server的Ip:Port
   * 該服務預設是使用Port:`9090`
   * `Prometheus`不需要特別配置，重點在`Grafana`
   * 詳情請參考連結
     * https://prometheus.io/
     * https://www.inwinstack.com/zh/blog-tw/blog_other-tw/2156/
 * Grafana(請用最新版)
   * 請根據相應平台進行安裝
   * 特性
     * `Grafana`是一個開源的分析與監控解決方案支援很多資料來源(如`Prometheus`、`Loki`)
     * 具備豐富的面板選擇
     * 可將監控的頁面儲存成樣板，以便在不同台主機上使用
     * 用網頁的方式呈現資料
     * 可以設定各種Role規則
     * 同樣支援`PromQL`查詢
     * 專案有多人在維護，有BUG容易得到解決
     * 更多特性請查閱參考連結
   * 安裝完成後，請配置`grafana.ini`文件並起動`Grafana`
     * `grafana.ini`文件主要是配置SMTP，用來做告警的配置
   * 啟動後(該服務預設是使用Port:`3000`)
     * 1.起動`Grafana`後，使用網頁進行登入(預設帳密admin/admin)
     * 2.再來進行資料來源地設置，這裡請選擇你架設的`Prometheus`的address
     * 3.之後新增監控面板(可以上grafana找現成的模板套用)，或自己定義面板來新增監測指標
   * 詳情請參考連結
     * https://grafana.com/
     * https://yunlzheng.gitbook.io/prometheus-book/part-ii-prometheus-jin-jie/grafana/grafana-panels
 
