const path = require('path');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
module.exports = {
	entry: ["./src/index.js", "./src/index.scss"],
	plugins: [
		new MiniCssExtractPlugin({ filename: 'site.css' }),
	],
	module: {
		rules: [
			{
				test: /\.s[ac]ss$/i,
				use: [
					// Creates `style` nodes from JS strings
					MiniCssExtractPlugin.loader,
					// Translates CSS into CommonJS
					"css-loader",
					// Compiles Sass to CSS
					{
						loader: "sass-loader",
						options: {
							sourceMap: true,
							additionalData: (content, loaderContext) => {
								if (loaderContext.resourcePath.includes("node_modules/@ministryofjustice")) {
									// Manually add node_modules prefix when importing third-party files
									return content.replace(
										'@import "node_modules/govuk-frontend/dist/govuk/base";',
										'@import "govuk-frontend/dist/govuk/base";'
									);
								}
								return content;
							}
						},
					},
				],
			},
			{ test: /\.css$/, use: ['style-loader', 'css-loader'] },
			{
				test: /\.(woff2?)$/i,
				use: [
					{
						loader: 'file-loader',
						options: {
							emitFile: false,
							name: '/assets/fonts/[name].[ext]'
						}
					}
				]
			},
			{
				test: /\.(jpe?g|png|gif|svg)$/i,
				use: [
					{
						loader: 'file-loader',
						options: {
							emitFile: false,
							name: '/assets/images/[name].[ext]'
						}
					}
				]
			},
		]
	},
	output: {
		path: path.resolve(__dirname, 'dist'),
		filename: 'site.js',
	}
};