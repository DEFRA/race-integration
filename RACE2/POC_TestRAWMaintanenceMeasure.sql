INSERT INTO [dbo].[RAW_MaintenanceMeasures]
           ([DocumentName]
           ,[ReservoirName]
           ,[Reference]
           ,[Comment]
           ,[LastModifiedDateTime]
           ,[Action]
           ,[MergedComment])
     VALUES
		 ('1000020120231201_20231220150912.pdf','Reservoir One', 'Item 1','Verified during site visit on 1st July 2023.','01/12/2023  15:09:12','All remote monitoring and instrumentation systems shall be checked and recalibrated, if necessary, on an annual basis.','TRUE'),
		 ('1000020120231201_20231220150912.pdf','Reservoir One', 'Item 2','No rubbish nor floating debris observed.','01/12/2023  15:09:12','Any rubbish or floating debris accumulating in front of the draw-off screens, against the face of the overflow weir or dam embankment, shall be removed in a timely manner.','TRUE'),
		 ('1000020120231201_20231220150912.pdf','Reservoir One', 'Item 3','Confirmed with ground keeping team.','01/12/2023  15:09:12','The downstream shoulder shall be mown regularly to prevent colonisation by undesirable flora and the grass height shall be kept under 250 mm.','TRUE'),
		 ('1000020120231201_20231220150912.pdf','Reservoir One', 'Item 4','Verified during site visit.','01/12/2023  15:09:12','The surface drains on the embankment face and mitres shall be maintained to ensure that they are free-flowing and fully functional.','TRUE'),
		 ('1000030120231201_20230530171836.pdf','Reservoir Two', 'Item 1','The actions included here are intended to be numerous and of medium/medium-long length.','05/30/2023  17:18:36','Short action','TRUE'),
		 ('1000030120231201_20230530171836.pdf','Reservoir Two', 'Item 2','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In porta consequat libero dignissim aliquam. Aliquam erat volutpat. Donec porta ipsum nisl, in ultrices ex accumsan finibus. Proin facilisis ligula eu condimentum laoreet. Fusce vestibulum ligula nec ligula suscipit, id eleifend velit porttitor. Fusce aliquet iaculis eros tristique dictum. Cras iaculis interdum nibh vel ullamcorper.','05/30/2023  17:18:36','Short-medium action where the supervising engineer will need to visit the reservoir at least twice a year','TRUE'),
		 ('1000030120231201_20230530171836.pdf','Reservoir Two', 'Item 3','Extra Long Comment made up of multiple paragraphs.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam eu suscipit neque, quis tristique diam. Praesent velit lectus, suscipit non ligula ut, aliquam auctor tellus. Phasellus a massa a erat ultrices varius. Suspendisse sagittis sodales arcu, ut fringilla nunc efficitur a. Duis id pretium ante. Aliquam sodales odio commodo felis faucibus finibus. Mauris in congue lacus, ut gravida augue. In et tempor justo, vitae finibus turpis. Aliquam lobortis et tellus at consectetur. Nunc mi nisi, placerat ornare mattis vel, vehicula id mauris. Nulla elementum, justo sit amet fermentum consectetur, libero ipsum varius nisl, a imperdiet lorem ex eu ex. Pellentesque eget consequat diam.','05/30/2023  17:18:36','Medium action where there are also multiple paragraphs.
Paragraph 2 begins here and ends here.Paragrah 3 begins here and carries on for short while longer to ensure this action spans multiple lines. Additional content is hereby included to turn this paragraph into a mulit-line paragraph.','TRUE'),
		 ('1000030120231201_20230530171836.pdf','Reservoir Two', 'Item 4','Verified during site visit.','05/30/2023  17:18:36','Extra Long Action made up of multiple paragraphs.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus a massa a erat ultrices varius. Suspendisse sagittis sodales arcu, ut fringilla nunc efficitur a. Duis id pretium ante. Aliquam sodales odio commodo felis faucibus finibus. Mauris in congue lacus, ut gravida augue. In et tempor justo, vitae finibus turpis. Aliquam lobortis et tellus at consectetur. Nunc mi nisi, placerat ornare mattis vel, vehicula id mauris. Nulla elementum, justo sit amet fermentum consectetur, libero ipsum varius nisl, a imperdiet lorem ex eu ex. Pellentesque eget consequat diam.Nullam a euismod magna. In luctus lorem at nulla venenatis malesuada. Cras neque tortor, semper id nisi sit amet, malesuada laoreet sem. Aliquam ac erat est. Etiam a mauris metus. Pellentesque id quam cursus, rhoncus magna et, accumsan elit. Duis est elit, hendrerit ut nibh vel, iaculis convallis massa.','TRUE'),
		 ('1000030120231201_20230530171836.pdf','Reservoir Two', 'Item 5','This measure has been evidenced through records.','05/30/2023  17:18:36','Action containing a cross reference to Section 4.3','TRUE'),
		 ('1000320120231201_20240115155126.pdf','Reservoir Thirty One', 'Item 1','This measure has been evidenced through records.','05/30/2023  17:18:36','The actions included here are intended to be numerous and of medium/medium-long length.','TRUE'),
		 ('1000320120231201_20240115155126.pdf','Reservoir Thirty One', 'Item 2','This measure has been evidenced through records.','05/30/2023  17:18:36','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In porta consequat libero dignissim aliquam. Aliquam erat volutpat. Donec porta ipsum nisl, in ultrices ex accumsan finibus. Proin facilisis ligula eu condimentum laoreet. Fusce vestibulum ligula nec ligula suscipit, id eleifend velit porttitor. Fusce aliquet iaculis eros tristique dictum. Cras iaculis interdum nibh vel ullamcorper.','TRUE'),
		  ('1000320120231201_20240115155126.pdf','Reservoir Thirty One', 'Item 3','This measure has been evidenced through records.','05/30/2023  17:18:36','Proin aliquam ex fermentum diam fermentum finibus. Nulla rutrum libero ac est viverra, non porta felis mattis. Quisque scelerisque urna nec libero aliquam, ac luctus neque congue. Quisque a elit sit amet enim feugiat accumsan. Aliquam non ante velit. Nullam rhoncus commodo metus, eget ullamcorper metus pellentesque at. Phasellus et libero et felis fermentum pharetra. Ut sit amet augue non enim vestibulum commodo nec at augue. Nulla non dui interdum, porttitor dolor vel, sollicitudin erat. Nunc in interdum lorem.','TRUE'),
		 ('1000320120231201_20240115155126.pdf','Reservoir Thirty One', 'Item 4','This measure has been evidenced through records.','05/30/2023  17:18:36','Sed tristique lectus dolor, eget mattis sapien vulputate vel. Donec eu facilisis massa. Vestibulum vehicula urna erat, a consectetur purus pellentesque sit amet. Suspendisse iaculis risus sit amet volutpat placerat. Integer et nisl sit amet nisi mattis vulputate sit amet quis est. Sed in hendrerit urna, id ullamcorper arcu. Cras id sem venenatis, luctus dui vitae, mattis mi. Nullam at congue tortor. Etiam quam metus, fermentum quis dignissim porttitor, vehicula non sapien. Mauris vel ornare lacus.','TRUE'),
		  ('1000320120231201_20240115155126.pdf','Reservoir Thirty One', 'Item 5','This measure has been evidenced through records.','05/30/2023  17:18:36','Sed vehicula nisl in eros bibendum ultricies. Nunc neque purus, congue sed aliquam quis, venenatis a odio. Proin a ex id ante blandit laoreet vitae et libero. Cras dignissim venenatis turpis a sodales. Proin semper maximus ipsum porta hendrerit. Ut ultrices vitae justo ac rutrum. Suspendisse et quam consectetur, lacinia quam in, tempor lectus. Phasellus viverra turpis bibendum risus egestas volutpat. Nulla ac pharetra ipsum. Sed non nulla nec tortor pellentesque fringilla iaculis eget odio. Proin molestie hendrerit purus, id tincidunt nisi laoreet sed.','TRUE'),
		 ('1000320120231201_20240115155126.pdf','Reservoir Thirty One', 'Item 6','This measure has been evidenced through records.','05/30/2023  17:18:36','Morbi in finibus lorem, id faucibus orci. Donec bibendum eleifend interdum. Sed magna orci, commodo a efficitur a, rhoncus eget mauris. Phasellus facilisis mollis luctus. In tincidunt nunc et porttitor mattis. Curabitur vel nisl nisi. Pellentesque venenatis pulvinar urna, vitae accumsan leo. Nunc efficitur massa commodo urna ultricies dictum. Donec sagittis dictum enim, at imperdiet nulla. In et nunc rhoncus ipsum ultrices egestas in eu velit. Phasellus pretium fermentum dui a consequat. Praesent aliquam lacus ut tellus finibus convallis.','TRUE'),
		 ('1000320120231201_20240115155126.pdf','Reservoir Thirty One', 'Item 7','This measure has been evidenced through records.','05/30/2023  17:18:36','Nulla lobortis sit amet tellus a lacinia. Cras vulputate non justo ut viverra. Suspendisse potenti. Vestibulum quis viverra erat, nec hendrerit nisi. Vestibulum feugiat enim eu nulla tempor congue. Etiam convallis vulputate nunc, ac molestie lorem maximus vitae. Aliquam metus nibh, lobortis et eleifend a, ultrices vitae eros. In hac habitasse platea dictumst. Sed et condimentum odio, sit amet vehicula ipsum. Integer sagittis urna a velit tincidunt, sed dapibus risus molestie. Sed vestibulum sem ut diam gravida porta. Mauris sed maximus purus, vitae volutpat quam.','TRUE'),
		 ('1000320120231201_20240115155126.pdf','Reservoir Thirty One', 'Item 8','This measure has been evidenced through records.','05/30/2023  17:18:36','In nec lectus vehicula, laoreet felis at, fermentum leo. Nulla facilisi. Donec eu nisi in urna luctus dignissim a ut massa. Etiam eget eros in purus finibus tempus vehicula finibus velit. Morbi sit amet nulla et elit rhoncus convallis in quis ex. Ut eu leo a tortor tristique dignissim sed non dolor. Nulla eget mi rutrum, eleifend justo id, commodo dui. Integer ut leo erat. Nullam eu leo dictum, eleifend odio vitae, tristique magna. Etiam vel leo lorem.','TRUE'),
		  ('1000320120231201_20240115155126.pdf','Reservoir Thirty One', 'Item 9','This measure has been evidenced through records.','05/30/2023  17:18:36','Aliquam sollicitudin imperdiet vestibulum. Mauris sagittis arcu ante, sed suscipit metus porta quis. Fusce cursus pharetra lacinia. Integer pretium massa dui, nec congue ipsum viverra id. Quisque condimentum dictum leo nec finibus. Nam tristique, magna id condimentum condimentum, nulla arcu bibendum enim, in porttitor nulla massa quis arcu. Nulla tristique fringilla massa eu malesuada. Pellentesque eu nisl quam. Nulla eget ex ac massa vestibulum egestas eget quis ex. Vivamus porttitor, augue sed efficitur laoreet, nunc justo tempor turpis, ut maximus justo dolor sit amet nibh. Fusce nec scelerisque nulla. Maecenas vel hendrerit nunc, sed consequat erat. Vestibulum mi diam, suscipit vitae leo sit amet, fermentum varius nunc. Vestibulum interdum lacus et nisi euismod ultricies. Vestibulum cursus metus sed auctor varius.','TRUE'),
		 ('1000320120231201_20240115155126.pdf','Reservoir Thirty One', 'Item 10','This measure has been evidenced through records.','05/30/2023  17:18:36','Aenean auctor tristique orci et dapibus. Donec hendrerit nisl eu lobortis efficitur. In semper sed ex eu volutpat. Vestibulum at magna non magna maximus ornare. Nullam hendrerit, diam eget gravida posuere, est lacus volutpat eros, id ornare lorem enim in sapien. Cras sed aliquam lectus, quis placerat arcu. Interdum et malesuada fames ac ante ipsum primis in faucibus. Curabitur luctus magna efficitur magna volutpat, a molestie diam commodo. Mauris in leo risus. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nunc et metus porttitor, euismod risus quis, ultricies leo. Aenean ut mollis metus. Nullam vitae interdum ex. Donec maximus vehicula dictum. Sed maximus luctus magna id egestas.','TRUE'),
		 ('1000320120231201_20240115155126.pdf','Reservoir Thirty One', 'Item 11','This measure has been evidenced through records.','05/30/2023  17:18:36','Nullam sit amet iaculis dui. Cras pellentesque porta volutpat. Proin ut volutpat velit. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Duis vitae est urna. Pellentesque feugiat sapien et ipsum facilisis, vel volutpat metus pretium. Fusce sed lorem maximus, commodo justo a, tristique odio. Etiam ac maximus lectus. Curabitur mattis condimentum nisl a gravida. Suspendisse potenti. Mauris euismod est sed ullamcorper efficitur.','TRUE'),
		  ('1000320120231201_20240115155126.pdf','Reservoir Thirty One', 'Item 12','This measure has been evidenced through records.','05/30/2023  17:18:36','Maecenas efficitur nec sem eget lacinia. Curabitur malesuada lacus sit amet lorem vestibulum iaculis. Vestibulum eu consequat nisl. Vestibulum porttitor elementum condimentum. Suspendisse rutrum orci non dictum imperdiet. Sed velit augue, ullamcorper at mi eget, fermentum ultricies lectus. Mauris eu elit eu metus scelerisque placerat eu sit amet dui. Fusce eu tincidunt lectus. Praesent euismod eleifend diam, sed iaculis nisl vehicula quis. Nunc at ornare lacus.','TRUE'),
		 ('1000320120231201_20240115155126.pdf','Reservoir Thirty One', 'Item 13','This measure has been evidenced through records.','05/30/2023  17:18:36','Curabitur aliquam, mauris quis auctor molestie, dolor enim convallis purus, quis vestibulum odio mauris ut justo. Vestibulum sodales nulla et rhoncus dictum. Etiam vulputate iaculis pretium. Donec eu varius nisi. In pretium, nisi a dictum rhoncus, orci dui tempor sem, at dapibus enim velit id ex. Vivamus commodo diam et commodo imperdiet. Donec porta gravida bibendum. Curabitur vitae tempus risus, ac porttitor mi. Maecenas sit amet nibh commodo, facilisis odio porta, cursus libero. Mauris condimentum lectus porttitor cursus rhoncus. Suspendisse non luctus enim. Etiam efficitur felis nulla, ut vehicula sem efficitur facilisis.','TRUE'),
		  ('1000320120231201_20240115155126.pdf','Reservoir Thirty One', 'Item 14','This measure has been evidenced through records.','05/30/2023  17:18:36','Nullam blandit purus a tincidunt egestas. In varius odio eget dolor lobortis, quis laoreet sem porta. Nam tristique placerat luctus. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Praesent non rhoncus felis. Donec eget ipsum vitae leo condimentum dignissim. Nulla sed rutrum augue. Sed auctor ullamcorper porttitor. Duis in ultrices orci. Vivamus vehicula diam vel neque congue faucibus. Maecenas ut viverra eros. Donec arcu enim, vehicula sed quam cursus, ultricies feugiat augue. In eu porttitor ipsum. Nam nec sem accumsan, faucibus erat eu, condimentum mi. In risus odio, sagittis id lobortis vel, rhoncus a orci. Praesent dignissim risus id viverra ornare.','TRUE'),
		   ('1000320120231201_20240115155126.pdf','Reservoir Thirty One', 'Item 15','This measure has been evidenced through records.','05/30/2023  17:18:36','Nunc commodo lacus in ipsum dignissim tristique. Quisque id semper mauris. Aliquam feugiat suscipit nisl, ac interdum elit vulputate sed. Nunc ut massa non nulla aliquam porta et ac ex. Ut luctus bibendum ex, eu mattis velit sodales vel. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Nullam tellus justo, dapibus eget arcu sed, porta auctor ex. Quisque consequat malesuada purus mollis interdum. Phasellus tempor eleifend ante et interdum. Cras non mauris in dolor euismod mollis quis nec quam.
Nullam posuere pellentesque posuere. Morbi maximus et diam et dignissim. Nulla pellentesque, eros eu mollis sollicitudin, lorem lorem fermentum leo, vitae iaculis mi tortor ut enim. Phasellus dictum ut lacus ullamcorper euismod. Mauris at diam elit.','TRUE'),
		    ('1000320120231201_20240115155126.pdf','Reservoir Thirty One', 'Item 16','This measure has been evidenced through records.','05/30/2023  17:18:36','Nunc et molestie turpis. Mauris dui justo, ultrices in velit non, malesuada pulvinar elit. Mauris ligula orci, dapibus at scelerisque id, eleifend quis metus. Ut maximus suscipit eros non condimentum. Aenean ut venenatis mi. Suspendisse ac ipsum lacinia, auctor nisl id, fermentum enim. Integer sed enim fringilla, sollicitudin lacus quis, malesuada nunc. Aenean ultrices rhoncus pellentesque. Quisque est est, varius id cursus vel, feugiat tempor augue.','TRUE'),
			('1000330120240101_20240120105955.pdf','Reservoir Thirty Two', 'Item 1','This measure has been evidenced through records.','01/20/2024  10:59:55','Both valves be fully operated at least twice a year.','TRUE'),
			('1000330120240101_20240120105955.pdf','Reservoir Thirty Two', 'Item 2','This measure has been evidenced through records.','01/20/2024  10:59:55','The area upstream of the spillway is maintained free of obstructions.','TRUE')

