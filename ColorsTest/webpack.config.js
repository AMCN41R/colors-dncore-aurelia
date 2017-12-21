const path = require("path");
const CleanWebpackPlugin = require("clean-webpack-plugin");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const ExtractTextPlugin = require("extract-text-webpack-plugin");
const { AureliaPlugin } = require("aurelia-webpack-plugin");
const { ProvidePlugin } = require("webpack");
const { TsConfigPathsPlugin, CheckerPlugin } = require("awesome-typescript-loader");

const outDir = path.resolve(__dirname, "wwwroot");
const srcDir = path.resolve(__dirname, "app");
const nodeModulesDir = path.resolve(__dirname, "node_modules");
const baseUrl = "/";

const extractVendorCss = new ExtractTextPlugin({ filename: "vendor.[contenthash].css", allChunks: true });
const extractAppCss = new ExtractTextPlugin({ filename: "app.[contenthash].css", allChunks: true });

module.exports = {
    resolve: {
        extensions: [".ts", ".js"],
        modules: [srcDir, "node_modules"],
    },
    entry: {
        app: ["aurelia-bootstrapper"],
        vendor: [
            "bootstrap"
        ]
    },
    output: {
        path: outDir,
        publicPath: baseUrl,
        filename: "[name].[hash].bundle.js",
        sourceMapFilename: "[name].[hash].bundle.map",
        chunkFilename: "[name].[hash].chunk.js"
    },
    module: {
        rules: [
            {
                test: /\.min\.css$/,
                use: extractVendorCss.extract({
                    fallback: "style-loader",
                    use: [
                        {
                            loader: "css-loader",
                            options: {
                                minimize: true,
                                sourceMap: false
                            }
                        }
                    ]
                }),
                issuer: /\.ts$/i
            },
            {
                test: /main\.scss$/,
                use: extractAppCss.extract({
                    fallback: "style-loader",
                    use: [
                        {
                            loader: "css-loader",
                            options: {
                                minimize: false,
                                sourceMap: true
                            }
                        },
                        {
                            loader: "sass-loader",
                            options: {
                                sourceMap: true
                            }
                        }
                    ]
                }),
                issuer: /\.ts$/i
            },
            { test: /\.html$/i, loader: "html-loader" },
            { test: /\.ts$/i, loader: "awesome-typescript-loader", exclude: nodeModulesDir },

            { test: /\.woff2(\?v=[0-9]\.[0-9]\.[0-9])?$/i, loader: "url-loader", options: { limit: 10000, mimetype: "application/font-woff2" } },
            { test: /\.woff(\?v=[0-9]\.[0-9]\.[0-9])?$/i, loader: "url-loader", options: { limit: 10000, mimetype: "application/font-woff" } },
            { test: /\.(ttf|eot|svg|otf)(\?v=[0-9]\.[0-9]\.[0-9])?$/i, loader: "url-loader" }
        ]
    },
    plugins: [
        new AureliaPlugin(),
        new TsConfigPathsPlugin(),
        new CheckerPlugin(),
        new ProvidePlugin({
            "$": "jquery",
            "jQuery": "jquery",
            "window.jQuery": "jquery"
        }),
        new HtmlWebpackPlugin({
            template: "index.ejs",
            filename: `${outDir}/index.html`
        }),
        extractVendorCss,
        extractAppCss,
        new CleanWebpackPlugin([outDir])
    ]
}