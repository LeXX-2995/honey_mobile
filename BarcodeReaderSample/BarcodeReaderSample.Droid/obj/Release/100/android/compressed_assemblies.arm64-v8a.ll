; ModuleID = 'obj\Release\100\android\compressed_assemblies.arm64-v8a.ll'
source_filename = "obj\Release\100\android\compressed_assemblies.arm64-v8a.ll"
target datalayout = "e-m:e-i8:8:32-i16:16:32-i64:64-i128:128-n32:64-S128"
target triple = "aarch64-unknown-linux-android"


%struct.CompressedAssemblyDescriptor = type {
	i32,; uint32_t uncompressed_file_size
	i8,; bool loaded
	i8*; uint8_t* data
}

%struct.CompressedAssemblies = type {
	i32,; uint32_t count
	%struct.CompressedAssemblyDescriptor*; CompressedAssemblyDescriptor* descriptors
}
@__CompressedAssemblyDescriptor_data_0 = internal global [960512 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_1 = internal global [658944 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_2 = internal global [97792 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_3 = internal global [276480 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_4 = internal global [568320 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_5 = internal global [253952 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_6 = internal global [240128 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_7 = internal global [187392 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_8 = internal global [113664 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_9 = internal global [173568 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_10 = internal global [1723904 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_11 = internal global [173568 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_12 = internal global [577024 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_13 = internal global [226816 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_14 = internal global [340480 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_15 = internal global [48640 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_16 = internal global [120832 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_17 = internal global [18432 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_18 = internal global [16384 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_19 = internal global [41984 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_20 = internal global [37376 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_21 = internal global [83456 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_22 = internal global [169472 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_23 = internal global [5120 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_24 = internal global [5120 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_25 = internal global [138752 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_26 = internal global [13824 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_27 = internal global [11264 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_28 = internal global [847872 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_29 = internal global [139776 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_30 = internal global [1542656 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_31 = internal global [16896 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_32 = internal global [23552 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_33 = internal global [11264 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_34 = internal global [34816 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_35 = internal global [66048 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_36 = internal global [59392 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_37 = internal global [43008 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_38 = internal global [33280 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_39 = internal global [45568 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_40 = internal global [32768 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_41 = internal global [2159104 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_42 = internal global [122880 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_43 = internal global [684544 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_44 = internal global [34816 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_45 = internal global [70656 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_46 = internal global [9216 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_47 = internal global [189440 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_48 = internal global [5120 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_49 = internal global [46080 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_50 = internal global [5120 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_51 = internal global [35328 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_52 = internal global [14752 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_53 = internal global [178176 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_54 = internal global [17920 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_55 = internal global [552960 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_56 = internal global [808448 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_57 = internal global [73216 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_58 = internal global [26112 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_59 = internal global [212480 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_60 = internal global [38912 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_61 = internal global [7680 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_62 = internal global [419328 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_63 = internal global [55808 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_64 = internal global [55296 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_65 = internal global [340992 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_66 = internal global [15264 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_67 = internal global [14848 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_68 = internal global [38912 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_69 = internal global [68096 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_70 = internal global [1409024 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_71 = internal global [922112 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_72 = internal global [53248 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_73 = internal global [17408 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_74 = internal global [470016 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_75 = internal global [17920 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_76 = internal global [79360 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_77 = internal global [585728 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_78 = internal global [9216 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_79 = internal global [44032 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_80 = internal global [175104 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_81 = internal global [15872 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_82 = internal global [15360 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_83 = internal global [16384 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_84 = internal global [17408 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_85 = internal global [36864 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_86 = internal global [424448 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_87 = internal global [13312 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_88 = internal global [40448 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_89 = internal global [57856 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_90 = internal global [26112 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_91 = internal global [1207296 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_92 = internal global [936448 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_93 = internal global [263048 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_94 = internal global [103424 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_95 = internal global [258048 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_96 = internal global [21880 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_97 = internal global [137608 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_98 = internal global [14216 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_99 = internal global [2041208 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_100 = internal global [2198016 x i8] zeroinitializer, align 1


; Compressed assembly data storage
@compressed_assembly_descriptors = internal global [101 x %struct.CompressedAssemblyDescriptor] [
	; 0
	%struct.CompressedAssemblyDescriptor {
		i32 960512, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([960512 x i8], [960512 x i8]* @__CompressedAssemblyDescriptor_data_0, i32 0, i32 0); data
	}, 
	; 1
	%struct.CompressedAssemblyDescriptor {
		i32 658944, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([658944 x i8], [658944 x i8]* @__CompressedAssemblyDescriptor_data_1, i32 0, i32 0); data
	}, 
	; 2
	%struct.CompressedAssemblyDescriptor {
		i32 97792, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([97792 x i8], [97792 x i8]* @__CompressedAssemblyDescriptor_data_2, i32 0, i32 0); data
	}, 
	; 3
	%struct.CompressedAssemblyDescriptor {
		i32 276480, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([276480 x i8], [276480 x i8]* @__CompressedAssemblyDescriptor_data_3, i32 0, i32 0); data
	}, 
	; 4
	%struct.CompressedAssemblyDescriptor {
		i32 568320, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([568320 x i8], [568320 x i8]* @__CompressedAssemblyDescriptor_data_4, i32 0, i32 0); data
	}, 
	; 5
	%struct.CompressedAssemblyDescriptor {
		i32 253952, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([253952 x i8], [253952 x i8]* @__CompressedAssemblyDescriptor_data_5, i32 0, i32 0); data
	}, 
	; 6
	%struct.CompressedAssemblyDescriptor {
		i32 240128, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([240128 x i8], [240128 x i8]* @__CompressedAssemblyDescriptor_data_6, i32 0, i32 0); data
	}, 
	; 7
	%struct.CompressedAssemblyDescriptor {
		i32 187392, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([187392 x i8], [187392 x i8]* @__CompressedAssemblyDescriptor_data_7, i32 0, i32 0); data
	}, 
	; 8
	%struct.CompressedAssemblyDescriptor {
		i32 113664, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([113664 x i8], [113664 x i8]* @__CompressedAssemblyDescriptor_data_8, i32 0, i32 0); data
	}, 
	; 9
	%struct.CompressedAssemblyDescriptor {
		i32 173568, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([173568 x i8], [173568 x i8]* @__CompressedAssemblyDescriptor_data_9, i32 0, i32 0); data
	}, 
	; 10
	%struct.CompressedAssemblyDescriptor {
		i32 1723904, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([1723904 x i8], [1723904 x i8]* @__CompressedAssemblyDescriptor_data_10, i32 0, i32 0); data
	}, 
	; 11
	%struct.CompressedAssemblyDescriptor {
		i32 173568, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([173568 x i8], [173568 x i8]* @__CompressedAssemblyDescriptor_data_11, i32 0, i32 0); data
	}, 
	; 12
	%struct.CompressedAssemblyDescriptor {
		i32 577024, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([577024 x i8], [577024 x i8]* @__CompressedAssemblyDescriptor_data_12, i32 0, i32 0); data
	}, 
	; 13
	%struct.CompressedAssemblyDescriptor {
		i32 226816, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([226816 x i8], [226816 x i8]* @__CompressedAssemblyDescriptor_data_13, i32 0, i32 0); data
	}, 
	; 14
	%struct.CompressedAssemblyDescriptor {
		i32 340480, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([340480 x i8], [340480 x i8]* @__CompressedAssemblyDescriptor_data_14, i32 0, i32 0); data
	}, 
	; 15
	%struct.CompressedAssemblyDescriptor {
		i32 48640, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([48640 x i8], [48640 x i8]* @__CompressedAssemblyDescriptor_data_15, i32 0, i32 0); data
	}, 
	; 16
	%struct.CompressedAssemblyDescriptor {
		i32 120832, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([120832 x i8], [120832 x i8]* @__CompressedAssemblyDescriptor_data_16, i32 0, i32 0); data
	}, 
	; 17
	%struct.CompressedAssemblyDescriptor {
		i32 18432, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([18432 x i8], [18432 x i8]* @__CompressedAssemblyDescriptor_data_17, i32 0, i32 0); data
	}, 
	; 18
	%struct.CompressedAssemblyDescriptor {
		i32 16384, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([16384 x i8], [16384 x i8]* @__CompressedAssemblyDescriptor_data_18, i32 0, i32 0); data
	}, 
	; 19
	%struct.CompressedAssemblyDescriptor {
		i32 41984, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([41984 x i8], [41984 x i8]* @__CompressedAssemblyDescriptor_data_19, i32 0, i32 0); data
	}, 
	; 20
	%struct.CompressedAssemblyDescriptor {
		i32 37376, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([37376 x i8], [37376 x i8]* @__CompressedAssemblyDescriptor_data_20, i32 0, i32 0); data
	}, 
	; 21
	%struct.CompressedAssemblyDescriptor {
		i32 83456, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([83456 x i8], [83456 x i8]* @__CompressedAssemblyDescriptor_data_21, i32 0, i32 0); data
	}, 
	; 22
	%struct.CompressedAssemblyDescriptor {
		i32 169472, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([169472 x i8], [169472 x i8]* @__CompressedAssemblyDescriptor_data_22, i32 0, i32 0); data
	}, 
	; 23
	%struct.CompressedAssemblyDescriptor {
		i32 5120, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([5120 x i8], [5120 x i8]* @__CompressedAssemblyDescriptor_data_23, i32 0, i32 0); data
	}, 
	; 24
	%struct.CompressedAssemblyDescriptor {
		i32 5120, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([5120 x i8], [5120 x i8]* @__CompressedAssemblyDescriptor_data_24, i32 0, i32 0); data
	}, 
	; 25
	%struct.CompressedAssemblyDescriptor {
		i32 138752, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([138752 x i8], [138752 x i8]* @__CompressedAssemblyDescriptor_data_25, i32 0, i32 0); data
	}, 
	; 26
	%struct.CompressedAssemblyDescriptor {
		i32 13824, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([13824 x i8], [13824 x i8]* @__CompressedAssemblyDescriptor_data_26, i32 0, i32 0); data
	}, 
	; 27
	%struct.CompressedAssemblyDescriptor {
		i32 11264, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([11264 x i8], [11264 x i8]* @__CompressedAssemblyDescriptor_data_27, i32 0, i32 0); data
	}, 
	; 28
	%struct.CompressedAssemblyDescriptor {
		i32 847872, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([847872 x i8], [847872 x i8]* @__CompressedAssemblyDescriptor_data_28, i32 0, i32 0); data
	}, 
	; 29
	%struct.CompressedAssemblyDescriptor {
		i32 139776, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([139776 x i8], [139776 x i8]* @__CompressedAssemblyDescriptor_data_29, i32 0, i32 0); data
	}, 
	; 30
	%struct.CompressedAssemblyDescriptor {
		i32 1542656, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([1542656 x i8], [1542656 x i8]* @__CompressedAssemblyDescriptor_data_30, i32 0, i32 0); data
	}, 
	; 31
	%struct.CompressedAssemblyDescriptor {
		i32 16896, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([16896 x i8], [16896 x i8]* @__CompressedAssemblyDescriptor_data_31, i32 0, i32 0); data
	}, 
	; 32
	%struct.CompressedAssemblyDescriptor {
		i32 23552, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([23552 x i8], [23552 x i8]* @__CompressedAssemblyDescriptor_data_32, i32 0, i32 0); data
	}, 
	; 33
	%struct.CompressedAssemblyDescriptor {
		i32 11264, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([11264 x i8], [11264 x i8]* @__CompressedAssemblyDescriptor_data_33, i32 0, i32 0); data
	}, 
	; 34
	%struct.CompressedAssemblyDescriptor {
		i32 34816, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([34816 x i8], [34816 x i8]* @__CompressedAssemblyDescriptor_data_34, i32 0, i32 0); data
	}, 
	; 35
	%struct.CompressedAssemblyDescriptor {
		i32 66048, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([66048 x i8], [66048 x i8]* @__CompressedAssemblyDescriptor_data_35, i32 0, i32 0); data
	}, 
	; 36
	%struct.CompressedAssemblyDescriptor {
		i32 59392, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([59392 x i8], [59392 x i8]* @__CompressedAssemblyDescriptor_data_36, i32 0, i32 0); data
	}, 
	; 37
	%struct.CompressedAssemblyDescriptor {
		i32 43008, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([43008 x i8], [43008 x i8]* @__CompressedAssemblyDescriptor_data_37, i32 0, i32 0); data
	}, 
	; 38
	%struct.CompressedAssemblyDescriptor {
		i32 33280, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([33280 x i8], [33280 x i8]* @__CompressedAssemblyDescriptor_data_38, i32 0, i32 0); data
	}, 
	; 39
	%struct.CompressedAssemblyDescriptor {
		i32 45568, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([45568 x i8], [45568 x i8]* @__CompressedAssemblyDescriptor_data_39, i32 0, i32 0); data
	}, 
	; 40
	%struct.CompressedAssemblyDescriptor {
		i32 32768, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([32768 x i8], [32768 x i8]* @__CompressedAssemblyDescriptor_data_40, i32 0, i32 0); data
	}, 
	; 41
	%struct.CompressedAssemblyDescriptor {
		i32 2159104, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([2159104 x i8], [2159104 x i8]* @__CompressedAssemblyDescriptor_data_41, i32 0, i32 0); data
	}, 
	; 42
	%struct.CompressedAssemblyDescriptor {
		i32 122880, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([122880 x i8], [122880 x i8]* @__CompressedAssemblyDescriptor_data_42, i32 0, i32 0); data
	}, 
	; 43
	%struct.CompressedAssemblyDescriptor {
		i32 684544, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([684544 x i8], [684544 x i8]* @__CompressedAssemblyDescriptor_data_43, i32 0, i32 0); data
	}, 
	; 44
	%struct.CompressedAssemblyDescriptor {
		i32 34816, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([34816 x i8], [34816 x i8]* @__CompressedAssemblyDescriptor_data_44, i32 0, i32 0); data
	}, 
	; 45
	%struct.CompressedAssemblyDescriptor {
		i32 70656, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([70656 x i8], [70656 x i8]* @__CompressedAssemblyDescriptor_data_45, i32 0, i32 0); data
	}, 
	; 46
	%struct.CompressedAssemblyDescriptor {
		i32 9216, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([9216 x i8], [9216 x i8]* @__CompressedAssemblyDescriptor_data_46, i32 0, i32 0); data
	}, 
	; 47
	%struct.CompressedAssemblyDescriptor {
		i32 189440, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([189440 x i8], [189440 x i8]* @__CompressedAssemblyDescriptor_data_47, i32 0, i32 0); data
	}, 
	; 48
	%struct.CompressedAssemblyDescriptor {
		i32 5120, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([5120 x i8], [5120 x i8]* @__CompressedAssemblyDescriptor_data_48, i32 0, i32 0); data
	}, 
	; 49
	%struct.CompressedAssemblyDescriptor {
		i32 46080, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([46080 x i8], [46080 x i8]* @__CompressedAssemblyDescriptor_data_49, i32 0, i32 0); data
	}, 
	; 50
	%struct.CompressedAssemblyDescriptor {
		i32 5120, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([5120 x i8], [5120 x i8]* @__CompressedAssemblyDescriptor_data_50, i32 0, i32 0); data
	}, 
	; 51
	%struct.CompressedAssemblyDescriptor {
		i32 35328, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([35328 x i8], [35328 x i8]* @__CompressedAssemblyDescriptor_data_51, i32 0, i32 0); data
	}, 
	; 52
	%struct.CompressedAssemblyDescriptor {
		i32 14752, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([14752 x i8], [14752 x i8]* @__CompressedAssemblyDescriptor_data_52, i32 0, i32 0); data
	}, 
	; 53
	%struct.CompressedAssemblyDescriptor {
		i32 178176, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([178176 x i8], [178176 x i8]* @__CompressedAssemblyDescriptor_data_53, i32 0, i32 0); data
	}, 
	; 54
	%struct.CompressedAssemblyDescriptor {
		i32 17920, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([17920 x i8], [17920 x i8]* @__CompressedAssemblyDescriptor_data_54, i32 0, i32 0); data
	}, 
	; 55
	%struct.CompressedAssemblyDescriptor {
		i32 552960, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([552960 x i8], [552960 x i8]* @__CompressedAssemblyDescriptor_data_55, i32 0, i32 0); data
	}, 
	; 56
	%struct.CompressedAssemblyDescriptor {
		i32 808448, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([808448 x i8], [808448 x i8]* @__CompressedAssemblyDescriptor_data_56, i32 0, i32 0); data
	}, 
	; 57
	%struct.CompressedAssemblyDescriptor {
		i32 73216, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([73216 x i8], [73216 x i8]* @__CompressedAssemblyDescriptor_data_57, i32 0, i32 0); data
	}, 
	; 58
	%struct.CompressedAssemblyDescriptor {
		i32 26112, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([26112 x i8], [26112 x i8]* @__CompressedAssemblyDescriptor_data_58, i32 0, i32 0); data
	}, 
	; 59
	%struct.CompressedAssemblyDescriptor {
		i32 212480, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([212480 x i8], [212480 x i8]* @__CompressedAssemblyDescriptor_data_59, i32 0, i32 0); data
	}, 
	; 60
	%struct.CompressedAssemblyDescriptor {
		i32 38912, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([38912 x i8], [38912 x i8]* @__CompressedAssemblyDescriptor_data_60, i32 0, i32 0); data
	}, 
	; 61
	%struct.CompressedAssemblyDescriptor {
		i32 7680, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([7680 x i8], [7680 x i8]* @__CompressedAssemblyDescriptor_data_61, i32 0, i32 0); data
	}, 
	; 62
	%struct.CompressedAssemblyDescriptor {
		i32 419328, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([419328 x i8], [419328 x i8]* @__CompressedAssemblyDescriptor_data_62, i32 0, i32 0); data
	}, 
	; 63
	%struct.CompressedAssemblyDescriptor {
		i32 55808, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([55808 x i8], [55808 x i8]* @__CompressedAssemblyDescriptor_data_63, i32 0, i32 0); data
	}, 
	; 64
	%struct.CompressedAssemblyDescriptor {
		i32 55296, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([55296 x i8], [55296 x i8]* @__CompressedAssemblyDescriptor_data_64, i32 0, i32 0); data
	}, 
	; 65
	%struct.CompressedAssemblyDescriptor {
		i32 340992, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([340992 x i8], [340992 x i8]* @__CompressedAssemblyDescriptor_data_65, i32 0, i32 0); data
	}, 
	; 66
	%struct.CompressedAssemblyDescriptor {
		i32 15264, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([15264 x i8], [15264 x i8]* @__CompressedAssemblyDescriptor_data_66, i32 0, i32 0); data
	}, 
	; 67
	%struct.CompressedAssemblyDescriptor {
		i32 14848, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([14848 x i8], [14848 x i8]* @__CompressedAssemblyDescriptor_data_67, i32 0, i32 0); data
	}, 
	; 68
	%struct.CompressedAssemblyDescriptor {
		i32 38912, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([38912 x i8], [38912 x i8]* @__CompressedAssemblyDescriptor_data_68, i32 0, i32 0); data
	}, 
	; 69
	%struct.CompressedAssemblyDescriptor {
		i32 68096, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([68096 x i8], [68096 x i8]* @__CompressedAssemblyDescriptor_data_69, i32 0, i32 0); data
	}, 
	; 70
	%struct.CompressedAssemblyDescriptor {
		i32 1409024, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([1409024 x i8], [1409024 x i8]* @__CompressedAssemblyDescriptor_data_70, i32 0, i32 0); data
	}, 
	; 71
	%struct.CompressedAssemblyDescriptor {
		i32 922112, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([922112 x i8], [922112 x i8]* @__CompressedAssemblyDescriptor_data_71, i32 0, i32 0); data
	}, 
	; 72
	%struct.CompressedAssemblyDescriptor {
		i32 53248, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([53248 x i8], [53248 x i8]* @__CompressedAssemblyDescriptor_data_72, i32 0, i32 0); data
	}, 
	; 73
	%struct.CompressedAssemblyDescriptor {
		i32 17408, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([17408 x i8], [17408 x i8]* @__CompressedAssemblyDescriptor_data_73, i32 0, i32 0); data
	}, 
	; 74
	%struct.CompressedAssemblyDescriptor {
		i32 470016, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([470016 x i8], [470016 x i8]* @__CompressedAssemblyDescriptor_data_74, i32 0, i32 0); data
	}, 
	; 75
	%struct.CompressedAssemblyDescriptor {
		i32 17920, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([17920 x i8], [17920 x i8]* @__CompressedAssemblyDescriptor_data_75, i32 0, i32 0); data
	}, 
	; 76
	%struct.CompressedAssemblyDescriptor {
		i32 79360, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([79360 x i8], [79360 x i8]* @__CompressedAssemblyDescriptor_data_76, i32 0, i32 0); data
	}, 
	; 77
	%struct.CompressedAssemblyDescriptor {
		i32 585728, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([585728 x i8], [585728 x i8]* @__CompressedAssemblyDescriptor_data_77, i32 0, i32 0); data
	}, 
	; 78
	%struct.CompressedAssemblyDescriptor {
		i32 9216, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([9216 x i8], [9216 x i8]* @__CompressedAssemblyDescriptor_data_78, i32 0, i32 0); data
	}, 
	; 79
	%struct.CompressedAssemblyDescriptor {
		i32 44032, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([44032 x i8], [44032 x i8]* @__CompressedAssemblyDescriptor_data_79, i32 0, i32 0); data
	}, 
	; 80
	%struct.CompressedAssemblyDescriptor {
		i32 175104, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([175104 x i8], [175104 x i8]* @__CompressedAssemblyDescriptor_data_80, i32 0, i32 0); data
	}, 
	; 81
	%struct.CompressedAssemblyDescriptor {
		i32 15872, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([15872 x i8], [15872 x i8]* @__CompressedAssemblyDescriptor_data_81, i32 0, i32 0); data
	}, 
	; 82
	%struct.CompressedAssemblyDescriptor {
		i32 15360, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([15360 x i8], [15360 x i8]* @__CompressedAssemblyDescriptor_data_82, i32 0, i32 0); data
	}, 
	; 83
	%struct.CompressedAssemblyDescriptor {
		i32 16384, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([16384 x i8], [16384 x i8]* @__CompressedAssemblyDescriptor_data_83, i32 0, i32 0); data
	}, 
	; 84
	%struct.CompressedAssemblyDescriptor {
		i32 17408, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([17408 x i8], [17408 x i8]* @__CompressedAssemblyDescriptor_data_84, i32 0, i32 0); data
	}, 
	; 85
	%struct.CompressedAssemblyDescriptor {
		i32 36864, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([36864 x i8], [36864 x i8]* @__CompressedAssemblyDescriptor_data_85, i32 0, i32 0); data
	}, 
	; 86
	%struct.CompressedAssemblyDescriptor {
		i32 424448, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([424448 x i8], [424448 x i8]* @__CompressedAssemblyDescriptor_data_86, i32 0, i32 0); data
	}, 
	; 87
	%struct.CompressedAssemblyDescriptor {
		i32 13312, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([13312 x i8], [13312 x i8]* @__CompressedAssemblyDescriptor_data_87, i32 0, i32 0); data
	}, 
	; 88
	%struct.CompressedAssemblyDescriptor {
		i32 40448, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([40448 x i8], [40448 x i8]* @__CompressedAssemblyDescriptor_data_88, i32 0, i32 0); data
	}, 
	; 89
	%struct.CompressedAssemblyDescriptor {
		i32 57856, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([57856 x i8], [57856 x i8]* @__CompressedAssemblyDescriptor_data_89, i32 0, i32 0); data
	}, 
	; 90
	%struct.CompressedAssemblyDescriptor {
		i32 26112, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([26112 x i8], [26112 x i8]* @__CompressedAssemblyDescriptor_data_90, i32 0, i32 0); data
	}, 
	; 91
	%struct.CompressedAssemblyDescriptor {
		i32 1207296, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([1207296 x i8], [1207296 x i8]* @__CompressedAssemblyDescriptor_data_91, i32 0, i32 0); data
	}, 
	; 92
	%struct.CompressedAssemblyDescriptor {
		i32 936448, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([936448 x i8], [936448 x i8]* @__CompressedAssemblyDescriptor_data_92, i32 0, i32 0); data
	}, 
	; 93
	%struct.CompressedAssemblyDescriptor {
		i32 263048, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([263048 x i8], [263048 x i8]* @__CompressedAssemblyDescriptor_data_93, i32 0, i32 0); data
	}, 
	; 94
	%struct.CompressedAssemblyDescriptor {
		i32 103424, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([103424 x i8], [103424 x i8]* @__CompressedAssemblyDescriptor_data_94, i32 0, i32 0); data
	}, 
	; 95
	%struct.CompressedAssemblyDescriptor {
		i32 258048, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([258048 x i8], [258048 x i8]* @__CompressedAssemblyDescriptor_data_95, i32 0, i32 0); data
	}, 
	; 96
	%struct.CompressedAssemblyDescriptor {
		i32 21880, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([21880 x i8], [21880 x i8]* @__CompressedAssemblyDescriptor_data_96, i32 0, i32 0); data
	}, 
	; 97
	%struct.CompressedAssemblyDescriptor {
		i32 137608, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([137608 x i8], [137608 x i8]* @__CompressedAssemblyDescriptor_data_97, i32 0, i32 0); data
	}, 
	; 98
	%struct.CompressedAssemblyDescriptor {
		i32 14216, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([14216 x i8], [14216 x i8]* @__CompressedAssemblyDescriptor_data_98, i32 0, i32 0); data
	}, 
	; 99
	%struct.CompressedAssemblyDescriptor {
		i32 2041208, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([2041208 x i8], [2041208 x i8]* @__CompressedAssemblyDescriptor_data_99, i32 0, i32 0); data
	}, 
	; 100
	%struct.CompressedAssemblyDescriptor {
		i32 2198016, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([2198016 x i8], [2198016 x i8]* @__CompressedAssemblyDescriptor_data_100, i32 0, i32 0); data
	}
], align 8; end of 'compressed_assembly_descriptors' array


; compressed_assemblies
@compressed_assemblies = local_unnamed_addr global %struct.CompressedAssemblies {
	i32 101, ; count
	%struct.CompressedAssemblyDescriptor* getelementptr inbounds ([101 x %struct.CompressedAssemblyDescriptor], [101 x %struct.CompressedAssemblyDescriptor]* @compressed_assembly_descriptors, i32 0, i32 0); descriptors
}, align 8


!llvm.module.flags = !{!0, !1, !2, !3, !4, !5}
!llvm.ident = !{!6}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"branch-target-enforcement", i32 0}
!3 = !{i32 1, !"sign-return-address", i32 0}
!4 = !{i32 1, !"sign-return-address-all", i32 0}
!5 = !{i32 1, !"sign-return-address-with-bkey", i32 0}
!6 = !{!"Xamarin.Android remotes/origin/d17-4 @ 13ba222766e8e41d419327749426023194660864"}
