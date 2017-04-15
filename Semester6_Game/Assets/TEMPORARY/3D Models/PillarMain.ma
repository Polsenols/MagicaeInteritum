//Maya ASCII 2017 scene
//Name: PillarMain.ma
//Last modified: Sat, Apr 15, 2017 06:38:38 PM
//Codeset: 1252
requires maya "2017";
currentUnit -l centimeter -a degree -t film;
fileInfo "application" "maya";
fileInfo "product" "Maya 2017";
fileInfo "version" "2017";
fileInfo "cutIdentifier" "201606150345-997974";
fileInfo "osv" "Microsoft Windows 8 Home Premium Edition, 64-bit  (Build 9200)\n";
fileInfo "license" "student";
createNode transform -n "Pillar";
	rename -uid "E16235E4-4D91-3B4B-22E6-D9AB072E9FCA";
	setAttr ".rp" -type "double3" 0 2.1227407455444336 0 ;
	setAttr ".sp" -type "double3" 0 2.1227407455444336 0 ;
createNode mesh -n "PillarShape" -p "Pillar";
	rename -uid "44489157-449D-459A-DCB1-BCA5CD2B1EB6";
	setAttr -k off ".v";
	setAttr -s 4 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.40561249852180481 0.46938739717006683 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr ".dr" 1;
createNode mesh -n "polySurfaceShape1" -p "Pillar";
	rename -uid "F1572F78-40E2-61DB-F6F5-CB85F5EC6135";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.40561249852180481 0.46938739717006683 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 78 ".uvst[0].uvsp[0:77]" -type "float2" 0.375 0 0.625 0 0.375
		 0.25 0.625 0.25 0.375 0.5 0.625 0.5 0.375 0.75 0.625 0.75 0.375 1 0.625 1 0.875 0
		 0.875 0.25 0.125 0 0.125 0.25 0.625 0.25 0.625 0.5 0.375 0.5 0.375 0.25 0.375 0.75
		 0.625 0.75 0.625 1 0.375 1 0.375 0.75 0.625 0.75 0.625 1 0.375 1 0.5727464 0.33286914
		 0.54213095 0.30225354 0.45786911 0.3022536 0.42725354 0.33286917 0.42725357 0.41713086
		 0.45786923 0.44774643 0.57274646 0.41713089 0.54213101 0.4477464 0.56377506 0.25
		 0.625 0.25 0.375 0.311225 0.375 0.25 0.625 0.43877506 0.625 0.5 0.436225 0.5 0.375
		 0.5 0.625 0.28917798 0.41417798 0.25 0.58582199 0.5 0.56377524 0.25 0.58582234 0.25
		 0.43622524 0.25 0.375 0.31122479 0.375 0.28917766 0.375 0.43877479 0.375 0.46082202
		 0.625 0.31122524 0.62500006 0.43877524 0.625 0.46082231 0.56377476 0.5 0.43622476
		 0.5 0.41417766 0.5 0.55885392 0.25 0.43622503 0.25 0.45764834 0.25 0.375 0.31614608
		 0.375 0.438775 0.375 0.41735169 0.625 0.311225 0.625 0.33264834 0.625 0.43385392
		 0.563775 0.5 0.54235166 0.5 0.44114608 0.5 0.54235214 0.25 0.44114652 0.25 0.375
		 0.33264786 0.375 0.43385354 0.625 0.31614649 0.625 0.41735214 0.55885351 0.5 0.45764786
		 0.5;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 26 ".pt";
	setAttr ".pt[12]" -type "float3" 7.4505806e-009 0 -7.4505806e-009 ;
	setAttr ".pt[13]" -type "float3" -3.7252903e-009 0 -3.7252903e-009 ;
	setAttr ".pt[14]" -type "float3" -7.4505806e-009 0 7.4505806e-009 ;
	setAttr ".pt[15]" -type "float3" 7.4505806e-009 0 7.4505806e-009 ;
	setAttr ".pt[24]" -type "float3" -5.5879354e-009 -1.8626451e-009 -9.3132257e-010 ;
	setAttr ".pt[25]" -type "float3" -9.3132257e-010 -1.8626451e-009 -5.5879354e-009 ;
	setAttr ".pt[26]" -type "float3" 0 -1.8626451e-009 -3.7252903e-009 ;
	setAttr ".pt[27]" -type "float3" 3.7252903e-009 -1.8626451e-009 0 ;
	setAttr ".pt[28]" -type "float3" 0 -1.8626451e-009 3.7252903e-009 ;
	setAttr ".pt[29]" -type "float3" -3.7252903e-009 -1.8626451e-009 0 ;
	setAttr ".pt[30]" -type "float3" 3.7252903e-009 -1.8626451e-009 0 ;
	setAttr ".pt[31]" -type "float3" 0 -1.8626451e-009 3.7252903e-009 ;
	setAttr ".pt[32]" -type "float3" 0 1.1175871e-008 1.8626451e-009 ;
	setAttr ".pt[34]" -type "float3" 0 1.1175871e-008 1.8626451e-009 ;
	setAttr ".pt[36]" -type "float3" 0 1.1175871e-008 1.8626451e-009 ;
	setAttr ".pt[38]" -type "float3" 0 1.1175871e-008 1.8626451e-009 ;
	setAttr ".pt[40]" -type "float3" 0 1.1175871e-008 1.8626451e-009 ;
	setAttr ".pt[42]" -type "float3" 0 1.1175871e-008 1.8626451e-009 ;
	setAttr ".pt[44]" -type "float3" 0 1.1175871e-008 7.4505806e-009 ;
	setAttr ".pt[45]" -type "float3" 0 4.6566129e-010 0 ;
	setAttr ".pt[46]" -type "float3" 0 1.1175871e-008 0 ;
	setAttr ".pt[47]" -type "float3" 0 4.6566129e-010 0 ;
	setAttr ".pt[48]" -type "float3" 0 9.3132257e-010 0 ;
	setAttr ".pt[49]" -type "float3" 0 4.6566129e-010 0 ;
	setAttr ".pt[50]" -type "float3" 0 9.3132257e-010 0 ;
	setAttr ".pt[51]" -type "float3" 0 4.6566129e-010 0 ;
	setAttr -s 72 ".vt[0:71]"  -0.5 0.19420981 0.5 0.5 0.19420981 0.5 -0.5 1.19420981 0.5
		 0.5 1.19420981 0.5 -0.5 1.19420981 -0.5 0.5 1.19420981 -0.5 -0.5 0.19420981 -0.5
		 0.5 0.19420981 -0.5 -0.5 1.19420993 0.5 0.5 1.19420993 0.5 0.5 1.19420993 -0.5 -0.5 1.19420993 -0.5
		 -0.41550207 1.19421327 0.41550207 0.41550207 1.19421327 0.41550207 0.41550207 1.19421327 -0.41550207
		 -0.41550207 1.19421327 -0.41550207 -0.5 0.19420981 -0.5 0.5 0.19420981 -0.5 0.5 0.19420981 0.5
		 -0.5 0.19420981 0.5 -0.59871989 0.0019054413 -0.59871989 0.59871989 0.0019054413 -0.59871989
		 0.59871989 0.0019054413 0.59871989 -0.59871989 0.0019054413 0.59871989 0.20151365 4.24357605 0.11670601
		 0.34626037 4.098829269 0.17666207 0.20151365 4.24357605 -0.11670601 0.34626037 4.098829269 -0.17666207
		 0.11670601 4.24357605 0.20151365 0.17666207 4.098829269 0.34626037 -0.11670601 4.24357605 0.20151365
		 -0.17666207 4.098829269 0.34626037 -0.20151365 4.24357605 0.11670601 -0.34626037 4.098829269 0.17666207
		 -0.20151365 4.24357605 -0.11670601 -0.34626037 4.098829269 -0.17666207 0.11670601 4.24357605 -0.20151365
		 0.17666207 4.098829269 -0.34626037 -0.11670601 4.24357605 -0.20151365 -0.17666207 4.098829269 -0.34626037
		 0.17666207 1.45855904 0.34626037 0.26266852 1.30028915 0.37119436 0.34626037 1.45855904 0.17666207
		 0.37119436 1.30028915 0.26266852 -0.34626037 1.45855904 0.17666209 -0.37119436 1.30028915 0.26266852
		 -0.17666207 1.45855904 0.34626037 -0.26266852 1.30028915 0.37119436 0.34626037 1.45855904 -0.17666207
		 0.37119436 1.30028915 -0.26266852 0.17666207 1.45855904 -0.34626037 0.26266852 1.30028915 -0.37119436
		 -0.17666207 1.45855904 -0.34626037 -0.26266852 1.30028915 -0.37119436 -0.34626037 1.45855904 -0.17666207
		 -0.37119436 1.30028915 -0.26266852 0.17549548 1.33013916 0.35872611 0.11731835 1.39232588 0.34626037
		 -0.17549548 1.33013916 0.35872611 -0.11731835 1.39232588 0.34626037 -0.35872611 1.33013916 0.17549548
		 -0.34626037 1.39232588 0.11731835 -0.35872611 1.33013916 -0.17549548 -0.34626037 1.39232588 -0.11731835
		 0.34626037 1.39232588 0.11731835 0.35872611 1.33013916 0.17549548 0.35872611 1.33013916 -0.17549548
		 0.34626037 1.39232588 -0.11731835 0.11731835 1.39232588 -0.34626037 0.17549548 1.33013916 -0.35872611
		 -0.17549548 1.33013916 -0.35872611 -0.11731835 1.39232588 -0.34626037;
	setAttr -s 132 ".ed[0:131]"  0 1 0 2 3 0 4 5 0 6 7 0 0 2 0 1 3 0 2 4 0
		 3 5 0 4 6 0 5 7 0 6 0 0 7 1 0 2 8 0 3 9 0 8 9 0 5 10 0 9 10 0 4 11 0 11 10 0 8 11 0
		 15 12 0 12 13 0 13 14 0 14 15 0 9 13 1 12 8 1 10 14 1 11 15 1 6 16 0 7 17 0 16 17 0
		 1 18 0 17 18 0 0 19 0 19 18 0 16 19 0 16 20 0 17 21 0 20 21 0 18 22 0 21 22 0 19 23 0
		 23 22 0 20 23 0 13 41 0 13 43 0 12 45 0 12 47 0 14 49 0 14 51 0 15 53 0 24 25 0 25 27 0
		 27 26 0 26 24 0 24 28 0 28 29 0 29 25 0 27 37 0 37 36 0 36 26 0 28 30 0 30 31 0 31 29 0
		 30 32 0 32 33 0 33 31 0 32 34 0 34 35 0 35 33 0 34 38 0 38 39 0 39 35 0 37 39 0 38 36 0
		 31 46 0 25 42 0 37 50 0 35 54 0 40 29 0 41 56 1 42 64 1 43 65 1 44 33 0 45 60 1 46 59 1
		 47 58 1 48 27 0 49 66 1 50 68 1 51 69 1 52 39 0 53 70 1 55 15 0 54 63 1 40 42 1 43 41 1
		 44 46 1 47 45 1 48 50 1 51 49 1 52 54 1 55 53 1 40 41 0 43 42 0 44 45 0 47 46 0 48 49 0
		 51 50 0 52 53 0 55 54 0 57 40 1 57 56 1 58 59 1 61 44 1 61 60 1 62 55 1 62 63 1 65 64 1
		 67 48 1 67 66 1 69 68 1 71 52 1 71 70 1 56 58 1 59 57 1 60 62 1 63 61 1 64 67 1 66 65 1
		 68 71 1 70 69 1;
	setAttr -s 62 -ch 264 ".fc[0:61]" -type "polyFaces" 
		f 4 0 5 -2 -5
		mu 0 4 0 1 3 2
		f 4 2 9 -4 -9
		mu 0 4 4 5 7 6
		f 4 38 40 -43 -44
		mu 0 4 22 23 24 25
		f 4 -12 -10 -8 -6
		mu 0 4 1 10 11 3
		f 4 10 4 6 8
		mu 0 4 12 0 2 13
		f 4 1 13 -15 -13
		mu 0 4 2 3 14 17
		f 4 7 15 -17 -14
		mu 0 4 3 5 15 14
		f 4 -3 17 18 -16
		mu 0 4 5 4 16 15
		f 4 -7 12 19 -18
		mu 0 4 4 2 17 16
		f 6 46 84 126 116 93 20
		mu 0 6 37 49 61 73 51 41
		f 4 14 24 -22 25
		mu 0 4 17 14 35 37
		f 4 16 26 -23 -25
		mu 0 4 14 15 39 35
		f 4 -19 27 -24 -27
		mu 0 4 15 16 41 39
		f 4 -20 -26 -21 -28
		mu 0 4 16 17 37 41
		f 4 3 29 -31 -29
		mu 0 4 6 7 19 18
		f 4 11 31 -33 -30
		mu 0 4 7 9 20 19
		f 4 -1 33 34 -32
		mu 0 4 9 8 21 20
		f 4 -11 28 35 -34
		mu 0 4 8 6 18 21
		f 4 30 37 -39 -37
		mu 0 4 18 19 23 22
		f 4 32 39 -41 -38
		mu 0 4 19 20 24 23
		f 4 -35 41 42 -40
		mu 0 4 20 21 25 24
		f 4 -36 36 43 -42
		mu 0 4 21 18 22 25
		f 3 96 -45 45
		mu 0 3 42 46 35
		f 3 98 -47 47
		mu 0 3 43 49 37
		f 3 100 -49 49
		mu 0 3 44 54 39
		f 3 102 -51 -94
		mu 0 3 51 57 41
		f 4 51 52 53 54
		mu 0 4 26 64 38 32
		f 4 -52 55 56 57
		mu 0 4 64 26 27 34
		f 4 -54 58 59 60
		mu 0 4 32 38 67 33
		f 4 -57 61 62 63
		mu 0 4 34 27 28 59
		f 4 -63 64 65 66
		mu 0 4 59 28 29 36
		f 4 -66 67 68 69
		mu 0 4 36 29 30 62
		f 4 -69 70 71 72
		mu 0 4 62 30 31 40
		f 4 -60 73 -72 74
		mu 0 4 33 67 40 31
		f 6 75 85 125 111 79 -64
		mu 0 6 59 47 60 70 45 34
		f 6 76 81 128 119 87 -53
		mu 0 6 64 52 65 75 53 38
		f 6 77 89 130 122 91 -74
		mu 0 6 67 55 68 77 56 40
		f 6 78 94 127 114 83 -70
		mu 0 6 62 50 63 72 48 36
		f 8 -65 -62 -56 -55 -61 -75 -71 -68
		mu 0 8 29 28 27 26 32 33 31 30
		f 4 -58 -80 95 -77
		mu 0 4 64 34 45 52
		f 4 -67 -84 97 -76
		mu 0 4 59 36 48 47
		f 4 -59 -88 99 -78
		mu 0 4 67 38 53 55
		f 4 -73 -92 101 -79
		mu 0 4 62 40 56 50
		f 6 129 -83 -46 22 48 88
		mu 0 6 66 74 42 35 39 54
		f 6 124 -87 -48 21 44 80
		mu 0 6 58 71 43 37 35 46
		f 6 131 -91 -50 23 50 92
		mu 0 6 69 76 44 39 41 57
		f 4 103 -97 104 -96
		mu 0 4 45 46 42 52
		f 4 105 -99 106 -98
		mu 0 4 48 49 43 47
		f 4 107 -101 108 -100
		mu 0 4 53 54 44 55
		f 4 109 -103 110 -102
		mu 0 4 56 57 51 50
		f 4 -112 112 -81 -104
		mu 0 4 45 70 58 46
		f 4 -105 82 118 -82
		mu 0 4 52 42 74 65
		f 4 -115 115 -85 -106
		mu 0 4 48 72 61 49
		f 4 -107 86 113 -86
		mu 0 4 47 43 71 60
		f 4 -120 120 -89 -108
		mu 0 4 53 75 66 54
		f 4 -109 90 121 -90
		mu 0 4 55 44 76 68
		f 4 -123 123 -93 -110
		mu 0 4 56 77 69 57
		f 4 -111 -117 117 -95
		mu 0 4 50 51 73 63
		f 4 -113 -126 -114 -125
		mu 0 4 58 70 60 71
		f 4 -116 -128 -118 -127
		mu 0 4 61 72 63 73
		f 4 -119 -130 -121 -129
		mu 0 4 65 74 66 75
		f 4 -122 -132 -124 -131
		mu 0 4 68 76 69 77;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
	setAttr ".dr" 1;
createNode groupId -n "groupId1";
	rename -uid "311B5CEB-4C1A-8344-3A8A-B6906DF150A9";
	setAttr ".ihi" 0;
createNode shadingEngine -n "lambert4SG";
	rename -uid "213E1B0E-418C-C1F4-821E-D29DAD3FF06C";
	setAttr ".ihi" 0;
	setAttr -s 2 ".dsm";
	setAttr ".ro" yes;
createNode materialInfo -n "materialInfo3";
	rename -uid "25697BB1-4128-158B-5CDF-DD9D3D494B4A";
createNode lambert -n "PillarBot";
	rename -uid "8A8A6868-4941-14DF-6CED-7A962FE908BE";
createNode file -n "file2";
	rename -uid "3177B68D-46B8-E298-62AB-878E957FCB7B";
	setAttr ".ftn" -type "string" "C:/Users/Evolee/Desktop/Magiae Interitum 3D objects/Addition Aseet exports and textures/Tower/brickpavement.jpg";
	setAttr ".cs" -type "string" "sRGB";
createNode place2dTexture -n "place2dTexture2";
	rename -uid "33D640D9-42BC-15B1-3ADF-2E98DDC4A5A8";
	setAttr ".re" -type "float2" 5 5 ;
createNode groupId -n "groupId3";
	rename -uid "32CD8C2B-4FAE-6C4F-52AA-619DE65114DF";
	setAttr ".ihi" 0;
createNode shadingEngine -n "lambert3SG";
	rename -uid "8999C9CC-431C-F9C7-A3E5-47B1467F317F";
	setAttr ".ihi" 0;
	setAttr ".ro" yes;
createNode materialInfo -n "materialInfo2";
	rename -uid "76415C0C-4273-7D0E-4DC5-D3BE89448D74";
createNode lambert -n "PillarTop";
	rename -uid "AEAA1AE7-4B17-06F0-1174-1FB5B8208632";
createNode file -n "file3";
	rename -uid "D9DCE539-413F-CF15-6EB9-1DAB96D36DF3";
	setAttr ".ftn" -type "string" "C:/Users/Evolee/Desktop/Stone_Tiled_1.jpg";
	setAttr ".cs" -type "string" "sRGB";
createNode place2dTexture -n "place2dTexture3";
	rename -uid "181960D6-4309-B657-EE9A-BC952B48386D";
	setAttr ".re" -type "float2" 3 3 ;
createNode groupParts -n "groupParts2";
	rename -uid "E31797BC-4905-5EF5-DB30-C9AED53F1F7B";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 2 "f[9]" "f[22:61]";
createNode groupParts -n "groupParts1";
	rename -uid "B5A92CDD-422B-0C8B-4DFD-B3A9036E92BE";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 2 "f[0:8]" "f[10:21]";
	setAttr ".irc" -type "componentList" 2 "f[9]" "f[22:61]";
createNode polyAutoProj -n "polyAutoProj1";
	rename -uid "2A2F039A-4DCC-9A94-E664-9CA3481CCE6A";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:61]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".s" -type "double3" 4.2416706085205078 4.2416706085205078 4.2416706085205078 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
createNode groupId -n "groupId2";
	rename -uid "70F25CD8-4904-3B11-70CC-9BBD2C187C2B";
	setAttr ".ihi" 0;
createNode lightLinker -s -n "lightLinker1";
	rename -uid "D52CE3C3-43D2-423C-024C-C1A3A44FFB63";
	setAttr -s 5 ".lnk";
	setAttr -s 5 ".slnk";
select -ne :time1;
	setAttr ".o" 1;
	setAttr ".unw" 1;
select -ne :hardwareRenderingGlobals;
	setAttr ".otfna" -type "stringArray" 22 "NURBS Curves" "NURBS Surfaces" "Polygons" "Subdiv Surface" "Particles" "Particle Instance" "Fluids" "Strokes" "Image Planes" "UI" "Lights" "Cameras" "Locators" "Joints" "IK Handles" "Deformers" "Motion Trails" "Components" "Hair Systems" "Follicles" "Misc. UI" "Ornaments"  ;
	setAttr ".otfva" -type "Int32Array" 22 0 1 1 1 1 1
		 1 1 1 0 0 0 0 0 0 0 0 0
		 0 0 0 0 ;
	setAttr ".fprt" yes;
select -ne :renderPartition;
	setAttr -s 5 ".st";
select -ne :renderGlobalsList1;
select -ne :defaultShaderList1;
	setAttr -s 6 ".s";
select -ne :postProcessList1;
	setAttr -s 2 ".p";
select -ne :defaultRenderUtilityList1;
	setAttr -s 3 ".u";
select -ne :defaultRenderingList1;
	setAttr -s 2 ".r";
select -ne :defaultTextureList1;
	setAttr -s 3 ".tx";
select -ne :initialShadingGroup;
	setAttr ".ro" yes;
	setAttr -s 2 ".gn";
select -ne :initialParticleSE;
	setAttr ".ro" yes;
select -ne :defaultResolution;
	setAttr ".pa" 1;
select -ne :hardwareRenderGlobals;
	setAttr ".ctrs" 256;
	setAttr ".btrs" 512;
select -ne :ikSystem;
	setAttr -s 4 ".sol";
connectAttr "groupId1.id" "PillarShape.iog.og[0].gid";
connectAttr "lambert4SG.mwc" "PillarShape.iog.og[0].gco";
connectAttr "groupId3.id" "PillarShape.iog.og[1].gid";
connectAttr "lambert3SG.mwc" "PillarShape.iog.og[1].gco";
connectAttr "groupParts2.og" "PillarShape.i";
connectAttr "groupId2.id" "PillarShape.ciog.cog[0].cgid";
connectAttr "PillarBot.oc" "lambert4SG.ss";
connectAttr "PillarShape.iog.og[0]" "lambert4SG.dsm" -na;
connectAttr "lambert4SG.msg" "materialInfo3.sg";
connectAttr "PillarBot.msg" "materialInfo3.m";
connectAttr "file2.msg" "materialInfo3.t" -na;
connectAttr "file2.oc" "PillarBot.c";
connectAttr ":defaultColorMgtGlobals.cme" "file2.cme";
connectAttr ":defaultColorMgtGlobals.cfe" "file2.cmcf";
connectAttr ":defaultColorMgtGlobals.cfp" "file2.cmcp";
connectAttr ":defaultColorMgtGlobals.wsn" "file2.ws";
connectAttr "place2dTexture2.c" "file2.c";
connectAttr "place2dTexture2.tf" "file2.tf";
connectAttr "place2dTexture2.rf" "file2.rf";
connectAttr "place2dTexture2.mu" "file2.mu";
connectAttr "place2dTexture2.mv" "file2.mv";
connectAttr "place2dTexture2.s" "file2.s";
connectAttr "place2dTexture2.wu" "file2.wu";
connectAttr "place2dTexture2.wv" "file2.wv";
connectAttr "place2dTexture2.re" "file2.re";
connectAttr "place2dTexture2.of" "file2.of";
connectAttr "place2dTexture2.r" "file2.ro";
connectAttr "place2dTexture2.n" "file2.n";
connectAttr "place2dTexture2.vt1" "file2.vt1";
connectAttr "place2dTexture2.vt2" "file2.vt2";
connectAttr "place2dTexture2.vt3" "file2.vt3";
connectAttr "place2dTexture2.vc1" "file2.vc1";
connectAttr "place2dTexture2.o" "file2.uv";
connectAttr "place2dTexture2.ofs" "file2.fs";
connectAttr "PillarTop.oc" "lambert3SG.ss";
connectAttr "PillarShape.iog.og[1]" "lambert3SG.dsm" -na;
connectAttr "groupId3.msg" "lambert3SG.gn" -na;
connectAttr "lambert3SG.msg" "materialInfo2.sg";
connectAttr "PillarTop.msg" "materialInfo2.m";
connectAttr "file3.msg" "materialInfo2.t" -na;
connectAttr "file3.oc" "PillarTop.c";
connectAttr ":defaultColorMgtGlobals.cme" "file3.cme";
connectAttr ":defaultColorMgtGlobals.cfe" "file3.cmcf";
connectAttr ":defaultColorMgtGlobals.cfp" "file3.cmcp";
connectAttr ":defaultColorMgtGlobals.wsn" "file3.ws";
connectAttr "place2dTexture3.c" "file3.c";
connectAttr "place2dTexture3.tf" "file3.tf";
connectAttr "place2dTexture3.rf" "file3.rf";
connectAttr "place2dTexture3.mu" "file3.mu";
connectAttr "place2dTexture3.mv" "file3.mv";
connectAttr "place2dTexture3.s" "file3.s";
connectAttr "place2dTexture3.wu" "file3.wu";
connectAttr "place2dTexture3.wv" "file3.wv";
connectAttr "place2dTexture3.re" "file3.re";
connectAttr "place2dTexture3.of" "file3.of";
connectAttr "place2dTexture3.r" "file3.ro";
connectAttr "place2dTexture3.n" "file3.n";
connectAttr "place2dTexture3.vt1" "file3.vt1";
connectAttr "place2dTexture3.vt2" "file3.vt2";
connectAttr "place2dTexture3.vt3" "file3.vt3";
connectAttr "place2dTexture3.vc1" "file3.vc1";
connectAttr "place2dTexture3.o" "file3.uv";
connectAttr "place2dTexture3.ofs" "file3.fs";
connectAttr "groupParts1.og" "groupParts2.ig";
connectAttr "groupId3.id" "groupParts2.gi";
connectAttr "polyAutoProj1.out" "groupParts1.ig";
connectAttr "groupId1.id" "groupParts1.gi";
connectAttr "polySurfaceShape1.o" "polyAutoProj1.ip";
connectAttr "PillarShape.wm" "polyAutoProj1.mp";
relationship "link" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" "lambert3SG.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" "lambert4SG.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" "lambert3SG.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" "lambert4SG.message" ":defaultLightSet.message";
connectAttr "lambert3SG.pa" ":renderPartition.st" -na;
connectAttr "lambert4SG.pa" ":renderPartition.st" -na;
connectAttr "PillarTop.msg" ":defaultShaderList1.s" -na;
connectAttr "PillarBot.msg" ":defaultShaderList1.s" -na;
connectAttr "place2dTexture2.msg" ":defaultRenderUtilityList1.u" -na;
connectAttr "place2dTexture3.msg" ":defaultRenderUtilityList1.u" -na;
connectAttr "file2.msg" ":defaultTextureList1.tx" -na;
connectAttr "file3.msg" ":defaultTextureList1.tx" -na;
connectAttr "PillarShape.ciog.cog[0]" ":initialShadingGroup.dsm" -na;
connectAttr "groupId1.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId2.msg" ":initialShadingGroup.gn" -na;
// End of PillarMain.ma
