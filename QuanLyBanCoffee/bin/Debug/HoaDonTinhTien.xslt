<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

	<xsl:template match="/HoaDonTinhTien">
		<html>
			<head>
				<meta charset="utf-8"/>
				<style>
					body {
					font-family: Arial, sans-serif;
					width: 400px;
					margin: 20px auto;
					padding: 20px;
					border: 1px solid #ccc;
					}
					.header {
					text-align: center;
					margin-bottom: 20px;
					}
					h1 {
					font-size: 24px;
					font-weight: bold;
					margin: 10px 0;
					}
					h2 {
					font-size: 18px;
					margin: 5px 0;
					}
					.info {
					margin: 10px 0;
					font-size: 14px;
					}
					.info-row {
					display: flex;
					justify-content: space-between;
					margin: 5px 0;
					}
					table {
					width: 100%;
					border-collapse: collapse;
					margin: 15px 0;
					}
					th, td {
					border: 1px solid #000;
					padding: 8px;
					text-align: left;
					}
					th {
					background-color: #f0f0f0;
					font-weight: bold;
					}
					.text-center {
					text-align: center;
					}
					.text-right {
					text-align: right;
					}
					.total {
					font-size: 20px;
					font-weight: bold;
					text-align: right;
					margin: 15px 0;
					}
					.footer {
					text-align: center;
					font-style: italic;
					margin-top: 20px;
					}
				</style>
			</head>
			<body>
				<div class="header">
					<h1>HÓA ĐƠN BÁN HÀNG</h1>
					<h2>
						Bàn <xsl:value-of select="SoBan"/>
					</h2>
				</div>

				<div class="info">
					<div class="info-row">
						<span>
							Ngày: <xsl:value-of select="Ngay"/>
						</span>
					</div>
					<div class="info-row">
						<span>
							Thu ngân: <xsl:value-of select="ThuNgan"/>
						</span>
						<span>
							In lúc: <xsl:value-of select="ThoiGianIn"/>
						</span>
					</div>
				</div>

				<table>
					<tr>
						<th>Mặt hàng</th>
						<th class="text-center">SL</th>
						<th class="text-right">Giá</th>
						<th class="text-right">T.tiền</th>
					</tr>
					<xsl:for-each select="ChiTiet/MatHang">
						<tr>
							<td>
								<xsl:value-of select="Ten"/>
							</td>
							<td class="text-center">
								<xsl:value-of select="SoLuong"/>
							</td>
							<td class="text-right">
								<xsl:value-of select="format-number(Gia, '#,###')"/>
							</td>
							<td class="text-right">
								<xsl:value-of select="format-number(ThanhTien, '#,###')"/>
							</td>
						</tr>
					</xsl:for-each>
				</table>

				<div class="total">
					Tổng: <xsl:value-of select="format-number(TongTien, '#,###')"/>
				</div>

				<div class="footer">
					Cảm ơn Quý khách. Hẹn gặp lại.!
				</div>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
