@echo off
echo ========================================
echo    UNITY 3D SURVIVAL GAME - CHECK TOOL
echo ========================================
echo.
echo Cong cu kiem tra cac van de cua project
echo.

REM Check if Unity is running
tasklist /FI "IMAGENAME eq Unity.exe" 2>NUL | find /I /N "Unity.exe">NUL
if "%ERRORLEVEL%"=="0" (
    echo [WARNING] Unity dang chay! Hay DONG Unity truoc khi thuc hien cac buoc sau!
    echo.
) else (
    echo [OK] Unity khong chay
    echo.
)

REM Check if Library folder exists
if exist "..\..\..\Library" (
    echo [FOUND] Thu muc Library ton tai
    echo.
    echo Neu gap loi, ban co the:
    echo 1. DONG Unity
    echo 2. XOA thu muc Library
    echo 3. MO LAI Unity
    echo.
) else (
    echo [INFO] Thu muc Library khong ton tai (Unity se tao lai)
    echo.
)

REM Check Packages folder
if exist "..\..\..\Packages" (
    echo [FOUND] Thu muc Packages ton tai
    echo.
) else (
    echo [ERROR] Khong tim thay thu muc Packages!
    echo.
)

REM Check manifest.json
if exist "..\..\..\Packages\manifest.json" (
    echo [FOUND] File manifest.json ton tai
    echo.
    echo Dang kiem tra noi dung...
    findstr /C:"com.unity.textmeshpro" "..\..\..\Packages\manifest.json" >NUL
    if "%ERRORLEVEL%"=="0" (
        echo   [OK] TextMeshPro da duoc cau hinh
    ) else (
        echo   [WARNING] TextMeshPro CHUA duoc cau hinh!
    )
    
    findstr /C:"com.unity.inputsystem" "..\..\..\Packages\manifest.json" >NUL
    if "%ERRORLEVEL%"=="0" (
        echo   [OK] Input System da duoc cau hinh
    ) else (
        echo   [WARNING] Input System CHUA duoc cau hinh!
    )
    
    findstr /C:"com.unity.ugui" "..\..\..\Packages\manifest.json" >NUL
    if "%ERRORLEVEL%"=="0" (
        echo   [OK] Unity UI da duoc cau hinh
    ) else (
        echo   [WARNING] Unity UI CHUA duoc cau hinh!
    )
    echo.
) else (
    echo [ERROR] Khong tim thay manifest.json!
    echo.
)

echo ========================================
echo            KET QUA KIEM TRA
echo ========================================
echo.
echo Neu thay [WARNING] hoac [ERROR]:
echo - Mo Unity Editor
echo - Vao Window ^> Package Manager  
echo - Cai dat cac packages: TextMeshPro, Input System, Unity UI
echo.
echo Chi tiet xem file: SUA_LOI_CHI_TIET.md
echo Huong dan nhanh: README_QUICK_FIX.md
echo.
echo ========================================
pause
