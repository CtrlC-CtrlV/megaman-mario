<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="/">
    <xsl:if test="./items/item[@type='lifetank']" >
      <xsl:attribute name="points">
    
      </xsl:attribute>
    </xsl:if>
    <xsl:if test="./items/item[@type='etank']" >
      <xsl:attribute name="points"></xsl:attribute>

    </xsl:if>
    <xsl:if test="./items/item[@type='megamanhelmet']" >
      <xsl:attribute name="points"></xsl:attribute>
    </xsl:if>
    <xsl:if test="./items/item[@type='falconpowerup']" >
      <xsl:attribute name="points"></xsl:attribute>
    </xsl:if>
    <xsl:if test="./items/item[@type='zerohelmet']" >
      <xsl:attribute name="points"></xsl:attribute>
    </xsl:if>
  </xsl:template>
</xsl:stylesheet>