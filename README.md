# cwr
Cmd Windows Regex – grep alternative

Created doing some parsing work by windows command-line.
Grep for Windows http://gnuwin32.sourceforge.net/packages/grep.htm doesn't support multiline (but it's still cool stuff), and Windows cmd has very poor UTF support. Cygwin was not an option.

So CWR was made to do both multiline parsing (using regex) and full UTF support.
Usage:
cwr inputfilename filewithregex outputfilename

Regex can be read from file only to prevent win cmd UTF-8 and mirroring issues.
No piping yet.
