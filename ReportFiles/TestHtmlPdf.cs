using ReportPOC.Models;
using System.Reflection;

namespace ReportPOC.ReportFiles
{
    public class TestHtmlPdf
    {
        public string GenerateSamplePdfTemplate(PdfModel model)
        {
            var htmlContent = $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>{model.Title}</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            margin: 20px;
            line-height: 1.6;
        }}
        h1 {{
            color: #333;
        }}
        table {{
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }}
        th, td {{
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }}
        th {{
            background-color: #f4f4f4;
        }}
        img {{
            max-width: 100%;
            height: auto;
        }}
    </style>
</head>
<body>
    <h1>{model.Title}</h1>
    <p>Name: {model.Name}</p>
    <p>Age: {model.Age}</p>
    <p>City: {model.City}</p>

 <img src='{model.Image}' alt='Dynamic Image' />
   
<h2>User List</h2>
    <table>
        <thead>
            <tr>
                <th>#</th>
                <th>Name</th>
                <th>Age</th>
                <th>City</th>
            </tr>
        </thead>
        <tbody>
            ";

            foreach (var user in model.Users)
            {
                htmlContent += $@"
            <tr>
                <td>{user.Id}</td>
                <td>{user.Name}</td>
                <td>{user.Age}</td>
                <td>{user.City}</td>
            </tr>";
            }

            htmlContent += @"
        </tbody>
    </table>

 <h2>Image Example</h2>
    <img src='https://images.unsplash.com/photo-1484788984921-03950022c9ef?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8bGFwdG9wfGVufDB8fDB8fHww' alt='Example Image' />
   <h1>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eu condimentum ipsum. Nam non lacus est. Integer varius eget odio vitae mattis. Morbi iaculis iaculis leo id rhoncus. Duis fermentum malesuada rutrum. Cras sed pellentesque libero, elementum ultricies orci. Maecenas at euismod felis. Morbi id ex rhoncus, euismod mi at, pulvinar velit. Suspendisse ut vehicula est, in dictum urna. Curabitur luctus cursus ipsum eu consequat. Ut vestibulum libero aliquam hendrerit rutrum. Morbi sit amet ipsum quis tortor porttitor pulvinar. Aenean bibendum molestie tincidunt. Praesent varius nibh in tristique elementum. Ut euismod posuere convallis. Mauris vitae diam erat.

Etiam sagittis condimentum quam, in accumsan erat interdum a. Praesent vitae massa sit amet odio viverra posuere. Nam ornare magna risus, eu euismod risus faucibus tincidunt. Fusce vitae ante non leo tempor pretium eget eget urna. Sed ligula quam, tristique quis est a, sodales convallis tortor. Sed et lacus urna. Etiam dapibus maximus lacus quis accumsan. Vestibulum viverra convallis est nec interdum. Curabitur vulputate sollicitudin neque, maximus accumsan urna ultrices ut. Aliquam tincidunt eu ipsum sit amet aliquet. Suspendisse in pharetra felis, sit amet vehicula sapien. Quisque venenatis ligula sapien, eu suscipit erat condimentum ac.

Quisque eu quam consectetur, consequat quam nec, commodo turpis. Aliquam erat volutpat. Mauris eu sapien a sapien varius tempus. Curabitur ut suscipit sapien. Donec id pretium risus. Vivamus vitae quam nec risus faucibus facilisis eget at justo. Vestibulum quis libero vitae purus elementum eleifend. Nullam rutrum ex hendrerit, porttitor tellus at, placerat leo. Nunc scelerisque, leo eget sodales luctus, erat ipsum egestas nibh, et laoreet ante arcu eget lectus. Phasellus tempor, magna id varius commodo, elit metus scelerisque augue, in consequat metus magna eget tellus. Vestibulum non pretium mauris, vel dignissim arcu. Maecenas varius nulla tellus, et scelerisque justo varius imperdiet. Fusce auctor pulvinar metus, quis dignissim mauris malesuada et. Nam porttitor mi ac ligula fringilla, vel dapibus urna venenatis. Cras pharetra blandit diam, at egestas ex cursus eu. Nam eu enim a nulla tincidunt sagittis.</h1>

</body>
</html>";
            return htmlContent;
        }
    }
}
