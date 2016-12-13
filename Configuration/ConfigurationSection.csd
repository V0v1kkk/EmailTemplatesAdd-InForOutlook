<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="d0ed9acb-0435-4532-afdd-b5115bc4d562" namespace="Mass_helper" xmlSchemaNamespace="Mass_helper" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
  </typeDefinitions>
  <configurationElements>
    <configurationSectionGroup name="massProblems" namespace="Mass_helper">
      <configurationSectionProperties>
        <configurationSectionProperty>
          <containedConfigurationSection>
            <configurationSectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/openMass" />
          </containedConfigurationSection>
        </configurationSectionProperty>
      </configurationSectionProperties>
    </configurationSectionGroup>
    <configurationSection name="openMass" namespace="Mass_helper" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="openMass">
      <elementProperties>
        <elementProperty name="CreditBroker" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="creditBroker" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/creditBroker" />
          </type>
        </elementProperty>
        <elementProperty name="SBankV" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="sBankV" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/SBankV" />
          </type>
        </elementProperty>
        <elementProperty name="SBankP" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="sBankP" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/SBankP" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="creditBroker" namespace="Mass_helper" documentation="Кредитный Брокер">
      <attributeProperties>
        <attributeProperty name="to" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="to" isReadOnly="false" defaultValue="&quot;ML.IT.CreditBrokerTechAlerts@maxus.ru&quot;">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="copy" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="copy" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="SBankV" namespace="Mass_helper" documentation="Связной Банк (выдача карт, оформление де-позитов)">
      <attributeProperties>
        <attributeProperty name="TO" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="tO" isReadOnly="false" defaultValue="&quot;ML.IT.BankTechAlerts@maxus.ru;&quot;">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="COPY" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="cOPY" isReadOnly="false" defaultValue="&quot;ML.OK.Info.Roznica@maxus.ru&quot;">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="SBankP" namespace="Mass_helper" documentation="Связной Банк (попол-нение карт)">
      <attributeProperties>
        <attributeProperty name="TO" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="tO" isReadOnly="false" defaultValue="&quot;ML.IT.BankTechAlerts@maxus.ru;&quot;">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="COPY" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="cOPY" isReadOnly="false" defaultValue="&quot;ML.OK.Info.Roznica@maxus.ru&quot;">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>