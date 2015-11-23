using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Permissions;
using Microsoft.Win32;
using System.Security;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.IO;

namespace LumbarRobot.Common
{
    public class FontsClearup
    {
        /// <summary>
        /// 获取系统文件位置
        /// </summary>
        [MethodImpl(MethodImplOptions.ForwardRef), SecurityCritical, SuppressUnmanagedCodeSecurity, DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        internal static extern int SHGetSpecialFolderPathW(IntPtr hwndOwner, StringBuilder lpszPath, int nFolder, int fCreate);

        /// <summary>
        /// 获取字体文件夹
        /// </summary>
        /// <returns></returns>
        private static string GetFontDir()
        {
            var lpszPath = new StringBuilder(260);
            return SHGetSpecialFolderPathW(IntPtr.Zero, lpszPath, 20, 0) != 0 ? lpszPath.ToString().ToUpperInvariant() : null;
        }

        public const string FontsRegistryPath =
                  @"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion\Fonts";
        public const string FontsLocalMachineRegistryPath =
                  @"Software\Microsoft\Windows NT\CurrentVersion\Fonts";

        /// <summary>
        /// 获取所有字体信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<FontInfo> ScanAllRegistryFonts()
        {
            var fontNames = new List<FontInfo>();
            new RegistryPermission(RegistryPermissionAccess.Read, FontsRegistryPath).Assert();
            try
            {
                var fontDirPath = GetFontDir();
                using (var key = Registry.LocalMachine.OpenSubKey(FontsLocalMachineRegistryPath))
                {
                    if (key == null)
                    {
                        return Enumerable.Empty<FontInfo>();
                    }
                    var valueNames = key.GetValueNames();
                    foreach (var valueName in valueNames)
                    {
                        var fontName = key.GetValue(valueName).ToString();
                        var fontInfo = new FontInfo
                        {
                            Name = valueName,
                            RegistryKeyPath = key.ToString(),
                            Value = fontName
                        };
                        try
                        {
                            var systemFontUri = new Uri(fontName, UriKind.RelativeOrAbsolute);
                            if (!systemFontUri.IsAbsoluteUri)
                            {
                                new Uri(Path.Combine(fontDirPath, fontName));
                            }
                        }
                        catch
                        {
                            fontInfo.IsCorrupt = true;
                        }
                        fontNames.Add(fontInfo);
                    }
                    key.Close();
                    key.Flush();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }
            return fontNames;
        }

        /// <summary>
        /// 获取所有异常字体信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<FontInfo> GetAllCorruptFonts()
        {
            var fonts = ScanAllRegistryFonts();
            return fonts.Where(f => f.IsCorrupt);
        }

        /// <summary>
        /// 整理字体信息
        /// </summary>
        /// <param name="p_corruptFonts"></param>
        public static void FixRegistryFonts(IEnumerable<FontInfo> p_corruptFonts = null)
        {
            IEnumerable<FontInfo> corruptFonts = p_corruptFonts;
            if (corruptFonts == null)
            {
                corruptFonts = GetAllCorruptFonts();
            }

            new RegistryPermission(RegistryPermissionAccess.Write, FontsRegistryPath).Assert();
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(FontsLocalMachineRegistryPath, true))
                {
                    if (key == null) return;
                    foreach (var corruptFont in corruptFonts)
                    {
                        if (!corruptFont.IsCorrupt) continue;
                        var fixedFontName = RemoveInvalidCharsFormFontName(corruptFont.Value);
                        key.SetValue(corruptFont.Name, fixedFontName, RegistryValueKind.String);
                    }
                    key.Close();
                    key.Flush();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
                ScanAllRegistryFonts();
            }
        }

        private static string RemoveInvalidCharsFormFontName(string fontName)
        {
            var invalidChars = Path.GetInvalidPathChars();
            var fontCharList = fontName.ToCharArray().ToList();
            fontCharList.RemoveAll(c => invalidChars.Contains(c));
            return new string(fontCharList.ToArray());
        }
    }

    public class FontInfo
    {
        public string RegistryKeyPath { get; set; }
        public bool IsCorrupt { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

    }
}
