using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace OracleDBUpdater.Commands.ConsoleCommands
{
    class GitHubCommand : ICommand
    {
        /// <summary> Name of command. </summary>
        public string Name => "github";

        /// <summary> Returns the manual, if there is no manual for this command, it will return null. </summary>
        public string Manual => ConsoleCommandManual.Manual(Name);

        public string Execute(string[] args)
        {
            string result;
            if (args.Length == 2 && args[1] == "update")
            {
                string get_update_files_url = $"https://api.github.com/repos/{Configuration.GetVariable("GITHUB_PATH")}";
                if (string.IsNullOrEmpty(get_update_files_url)) { result = "E: Please check GITHUB_PATH in config or set it!"; }
                else
                {
                    string parse_result = ParseURL(get_update_files_url);
                    if (parse_result != null)
                    {
                        try
                        {
                            var json_result = JArray.Parse(parse_result);
                            result = "Start loading files:\n";
                            foreach (object file in json_result)
                            {
                                var obj_file = JObject.Parse(file.ToString());
                                string name = obj_file.SelectToken("name").ToString();
                                if (name.ToLower().Contains(".sql"))
                                {
                                    string parse_file = ParseURL(obj_file.SelectToken("git_url").ToString());
                                    if (parse_file == null) { continue; }
                                    else
                                    {
                                        try
                                        {
                                            parse_file = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(JObject.Parse(parse_file).SelectToken("content").ToString()));
                                            File.WriteAllText($"{Configuration.GetVariable("UpdateFolder")}\\{name}", parse_file);
                                            result += $"Success added file: {name}\n";
                                        }
                                        catch (Exception ex)
                                        {
                                            result += $"E: Can't get content of file: {ex.Message}\n";
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                            result = "E: Can't parse answer!";
                        }
                    }
                    else
                    {
                        result = "E: Can't get answer from github!";
                    }
                }
            }
            else
            {
                result = Manual;
            }
            return result;
        }

        private string ParseURL(string url)
        {
            string result = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    result = readStream.ReadToEnd();
                    readStream.Close();
                    readStream.Dispose();
                }
            }
            catch { }

            return result;
        }
    }
}