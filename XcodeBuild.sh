#工程名
project_name=XCodeProject
scheme_name=Unity-iPhone

#证书
code_sign_identiy=''
development_team=''
provisioning_profile_specifier=''

#打包模式 Debug/Release
development_mode=Debug

#plist路径
exportOptionsPlistPath=./OptionsPlist.plist 

#导出路径
exportFilePath=~/Desktop/$project_name-ipa

echo '开始打包' 
xcodebuild \
clean -configuration ${development_mode} -quiet  || exit 

xcodebuild archive \
-scheme ${scheme_name} \
-configuration ${development_mode} \
-archivePath build/${project_name}.xcarchive \
CODE_SIGN_STYLE="Manual" \
DEVELOPMENT_TEAM= ${development_team}\
CODE_SIGN_IDENTITY=${code_sign_identiy} \
PROVISIONING_PROFILE_SPECIFIER=${provisioning_profile_specifier} -quiet  || exit

xcodebuild -exportArchive -archivePath build/${project_name}.xcarchive \
-configuration ${development_mode} \
-exportPath ${exportFilePath} \
-exportOptionsPlist ${exportOptionsPlistPath} \
-quiet || exit

echo '打包完成'
