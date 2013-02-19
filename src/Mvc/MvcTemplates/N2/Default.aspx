﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="N2.Management.Default" %>
<%@ Register TagPrefix="edit" Namespace="N2.Edit.Web.UI.Controls" Assembly="N2.Management" %>
<!DOCTYPE html>
<!--[if lt IE 7]>      <html class="no-js lt-ie9 lt-ie8 lt-ie7"> <![endif]-->
<!--[if IE 7]>         <html class="no-js lt-ie9 lt-ie8"> <![endif]-->
<!--[if IE 8]>         <html class="no-js lt-ie9"> <![endif]-->
<!--[if gt IE 8]><!-->
<html class="no-js">
<!--<![endif]-->
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>N2 Management</title>
    <meta name="viewport" content="width=device-width">

    <%--<link rel="stylesheet" href="redesign/css/normalize.min.css">--%>
    <%--<link rel="stylesheet" type="text/css" href="http://yui.yahooapis.com/2.9.0/build/reset/reset-min.css">--%>
    <link rel="stylesheet" href="redesign/bootstrap/css/bootstrap.min.css">
	<style>
		ul,ol { padding:0; margin:0; list-style-type:none }
	</style>
    <link rel="stylesheet" href="redesign/css/n2.css">

    <script src="redesign/js/vendor/modernizr-2.6.2.min.js"></script>
</head>
<body>
    <div id="page">
        <div id="top-area">
            <div id="logo"><a href="http://n2cms.com">N2 CMS</a></div>
            <div id="user-tools">
                <ul class="menu">
                    <li><a href="#">Settings</a></li>
                    <li><a href="#">Sign out</a></li>
                </ul>
                <a href="#" class="current-user">Andreas Brantmo</a>
            </div>
            <div id="main-menu">
                <ul class="menu">
                    <li><a href="#">Dashboard</a></li>
                    <li class="active"><a href="#">Content</a></li>
                    <li><a href="#">Site</a></li>
                    <li><a href="#">System</a></li>
                </ul>
                <ul class="menu submenu">
                    <li class="active"><a href="#">Pages</a></li>
                    <li><a href="#">Files</a></li>
                    <li><a href="#">Wizards</a></li>
                </ul>

            </div>
        </div>
    </div>
    <div class="sliding-loader loader"></div>
    <div id="main-wrapper">
        <div id="secondary-area">
            <div class="search">
                <input type="text" placeholder="Search" /><input type="button" value="Search" />
            </div>
            <div id="page-tree">
                <ul class="site">
                    <li class="root expanded">
                        <div class="item">
                            <a href="#">demo.n2cms.com</a>
                            <div class="toggle">
                                <img src="redesign/img/minus.png" class="collapse" /><img src="redesign/img/plus.png" class="expand" />
                            </div>
                            <div class="tools">
                                <a href="#">Tools</a>
                            </div>
                        </div>
                        <ul>
                            <li class="expanded">
                                <div class="item">
                                    <a href="#">
                                        <img src="redesign/img/icons/page-world.png" />Home</a>
                                    <div class="toggle">
                                        <img src="redesign/img/minus.png" class="collapse" /><img src="redesign/img/plus.png" class="expand" />
                                    </div>
                                    <div class="tools">
                                        <a href="#">Tools</a>
                                    </div>
                                </div>
                                <ul>
                                    <li class="active expanded">
                                        <div class="item">
                                            <a href="#">
                                                <img src="redesign/img/icons/page.png" />Features</a>
                                            <div class="toggle">
                                                <img src="redesign/img/minus.png" class="collapse" title="Click to collapse children" /><img src="redesign/img/plus.png" class="expand" />
                                            </div>
                                            <div class="tools">
                                                <a href="#">Tools</a>
                                            </div>
                                        </div>
                                        <ul>
                                            <li>
                                                <div class="item">
                                                    <a href="#">
                                                        <img src="redesign/img/icons/page.png" />Content page</a>
                                                    <div class="toggle">
                                                        <img src="redesign/img/minus.png" class="collapse" /><img src="redesign/img/plus.png" class="expand" />
                                                    </div>
                                                    <div class="tools">
                                                        <a href="#">Tools</a>
                                                    </div>
                                                </div>
                                            </li>
                                            <li class="collapsed">
                                                <div class="item">
                                                    <a href="#">
                                                        <img src="redesign/img/icons/page.png" />News</a>
                                                    <div class="toggle">
                                                        <img src="redesign/img/minus.png" class="collapse" /><img src="redesign/img/plus.png" class="expand" title="Click to expand children" />
                                                    </div>
                                                    <div class="tools">
                                                        <a href="#">Tools</a>
                                                    </div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="item">
                                                    <a href="#">
                                                        <img src="redesign/img/icons/page.png" />Calendar</a>
                                                    <div class="toggle">
                                                        <img src="redesign/img/minus.png" class="collapse" /><img src="redesign/img/plus.png" class="expand" />
                                                    </div>
                                                    <div class="tools">
                                                        <a href="#">Tools</a>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>
                                    </li>
                                    <li>
                                        <div class="item">
                                            <a href="#">
                                                <img src="redesign/img/icons/page.png" />Credits</a>
                                            <div class="toggle">
                                                <img src="redesign/img/minus.png" class="collapse" /><img src="redesign/img/plus.png" class="expand" />
                                            </div>
                                            <div class="tools">
                                                <a href="#">Tools</a>
                                            </div>
                                        </div>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
            <div id="trash">
                <div>
                    <div><a href="#">Trash (4 items)</a></div>
                    drag item here to delete
                </div>
            </div>
            <div class="dragbar"></div>
        </div>
        <div id="main-area">
            <div id="page-action-bar" class="action-bar secondary">
                <ul class="menu">
                    <li class="page-view iconed"><a href="#">
                        <img src="redesign/img/actionbar/view.png" /></a></li>
                    <li class="page-edit command-options iconed">
                        <a href="#">
                            <img src="redesign/img/actionbar/edit.png" /><div>Edit<span>Organize parts</span></div>
                        </a>
                    </li>
                    <li class="page-version command-options iconed">
                        <a href="#">
                            <img src="redesign/img/actionbar/version.png" /><div>Version<span>Published version</span></div>
                        </a>
                        <div class="command-options">
                            <ul>
                                <li>
                                    <a href="#">Draft #1<span class="meta">by Andreas Brantmo (ME)</span></a>
                                </li>
                                <li class="active">
                                    <a href="#">Published version<span class="meta">by John Swiss</span></a>
                                </li>
                                <li>
                                    <a href="#">12/7/13<span class="meta">by John Swiss</span></a>
                                </li>
                                <li>
                                    <a href="#">12/6/13<span class="meta">by John Swiss</span></a>
                                </li>
                                <li class="load-more">
                                    <a href="#"></a>
                                </li>
                            </ul>
                        </div>
                    </li>
                    <li class="page-language command-options iconed">
                        <a href="#" title="Select from available languages or create a new language version for this page.">
                            <img src="redesign/img/actionbar/language.png" /><div>Language<span>English</span></div>
                        </a>
                    </li>
                </ul>
                <div id="Div1" class="action-bar primary">
                    <ul>
                        <li class="page-publish iconed"><a href="#">
                            <img src="redesign/img/actionbar/publish.png" /><div>Publish</div>
                        </a></li>
                    </ul>
                </div>
                <div id="main-area-content">
                </div>
            </div>
        </div>
    </div>

    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script>window.jQuery || document.write('<script src="js/vendor/jquery-1.9.1.min.js"><\/script>')</script>

    <script src="redesign/js/plugins.js"></script>
    <script src="redesign/js/n2.js"></script>
    <script src="redesign/js/n2.loaders.js"></script>
    <script src="redesign/js/main.js"></script>
</body>
</html>
