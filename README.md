# BlogReader
Reads the contents of a website and extracts them into seperate files

A friend of mine asked me if i could help her print the contents of a Blog.
Said Blog was created with [Wordpress](https://github.com/WordPress/WordPress), but was lacking the option to view its contents in chronological order, or to see all posts in a summary altogether. It had, however, the option to list a number of posts in a page view, so i decided to use that to my advantage and wrote a litte program.

So, the attempt for this project was:
- Get all HTML content from different pages of the blog.
- extract the 'article' tags and content from the page HTML
- order articles in chronological order
- export articles as new HTML files
- print new HTML files to PDF or ePub for viewing pleasure on ebook-reader devices