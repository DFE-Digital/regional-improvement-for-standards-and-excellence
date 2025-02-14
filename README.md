
# regional improvement for standards and excellence

## Overview

This project is a **.NET Razor application** that integrates a **Node.js**-based build pipeline to manage frontend CSS. The Node setup leverages modern tools (like PostCSS, Sass) to build and optimize CSS assets, which are then used by the Razor views.

---

## Prerequisites

Before getting started, ensure the following tools are installed on your system:

1. **.NET SDK** (version 6.0 or higher)
2. **Node.js** (version 16 or higher)
3. **NPM** (comes with Node.js installation)

---

## Project Structure

```plaintext
|-- /wwwroot                # Static files served by the Razor app
|   |-- /css                # Final built CSS files
|-- /Node                   # Node.js files and configs
|   |-- package.json        # NPM dependencies and scripts
|   |-- /src                # Source CSS/SCSS files
|   |-- /dist               # Compiled and optimized CSS
|-- /Pages                  # Razor pages
|-- /Views                  # Razor views
|-- /Controllers            # Controllers for the app
|-- appsettings.json        # App configuration
```

---

## Setup Instructions

### 1. Clone the Repository

```bash
git clone <repository-url>
cd <repository-folder>
```

### 2. Install Dependencies

#### Backend Dependencies:
Run the following command to restore .NET dependencies:
```bash
dotnet restore
```

#### Frontend Dependencies:
Navigate to the `Node` directory and install Node.js dependencies:
```bash
cd Node
npm install
```

---

## Development Workflow

### 1. Build and Watch Frontend CSS

Run the following command in the `Node` directory to watch and build CSS changes:
```bash
npm run dev
```

This ensures the source CSS (in `/Node/src`) is processed and outputted to `/Node/dist`. These files are then copied to the `wwwroot/css` folder for use by Razor views.

### 2. Run the Razor App

From the project root, execute the following command:
```bash
dotnet run
```

The application will be available at `https://localhost:5001` or `http://localhost:5000`.

---

## NPM Scripts

- **`npm run dev`**  
  Watches CSS files, rebuilds on changes, and copies the output to the `wwwroot/css` directory.

- **`npm run build`**  
  Builds and optimizes CSS for production.

- **`npm run clean`**  
  Cleans the `dist` folder and resets build files.

---

## Adding CSS to Razor Views

Reference the built CSS in your Razor views like this:

```html
<link rel="stylesheet" href="css/styles.css" />
```

Make sure the paths match the files output to `/wwwroot/css` after the build.

---

## Tools Used

### .NET Razor:
- Backend framework for building server-side web applications.

### Node.js and Frontend Tools:
- **PostCSS**: For CSS transformations.

---

## Troubleshooting

- **CSS not updating:**  
  Ensure `npm run dev` is running in the `Node` directory and that the output files are being copied to `/wwwroot/css`.

- **Dependency issues:**  
  Check your Node.js or .NET SDK versions and ensure they match the prerequisites.

---

### Linting Sonar rules

Include the following extension in your IDE installation: [SonarQube for IDE](https://marketplace.visualstudio.com/items?itemName=SonarSource.sonarlint-vscode)

Update your [settings.json file](https://code.visualstudio.com/docs/getstarted/settings#_settings-json-file) to include the following

```json
"sonarlint.connectedMode.connections.sonarcloud": [   
    {
        "connectionId": "DfE",
        "organizationKey": "dfe-digital",
        "disableNotifications": false
    }   
]
```

Then follow [these steps](https://youtu.be/m8sAdYCIWhY) to connect to the SonarCloud instance.

## Contributions

Feel free to submit issues or pull requests to improve this project. 

---

## License

This project is licensed under the [MIT License](LICENSE).
