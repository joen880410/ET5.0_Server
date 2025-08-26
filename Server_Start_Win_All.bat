cd %cd%/Publish/win-x64

dotnet App.dll --appId=1 --appType=Manager --config=../../Config/StartConfig/Release.txt --configOther=../../Config/{0}.txt
pause