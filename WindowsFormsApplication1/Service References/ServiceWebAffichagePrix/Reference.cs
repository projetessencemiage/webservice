﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18034
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsFormsApplication1.ServiceWebAffichagePrix {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Station", Namespace="http://schemas.datacontract.org/2004/07/FuelTracker_Lib")]
    [System.SerializableAttribute()]
    public partial class Station : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string addressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string cityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string code_postalField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WindowsFormsApplication1.ServiceWebAffichagePrix.Enseigne enseigneField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string id_enseigneField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string id_stationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float lattitudeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float longitudeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WindowsFormsApplication1.ServiceWebAffichagePrix.Prix[] price_listField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string address {
            get {
                return this.addressField;
            }
            set {
                if ((object.ReferenceEquals(this.addressField, value) != true)) {
                    this.addressField = value;
                    this.RaisePropertyChanged("address");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string city {
            get {
                return this.cityField;
            }
            set {
                if ((object.ReferenceEquals(this.cityField, value) != true)) {
                    this.cityField = value;
                    this.RaisePropertyChanged("city");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string code_postal {
            get {
                return this.code_postalField;
            }
            set {
                if ((object.ReferenceEquals(this.code_postalField, value) != true)) {
                    this.code_postalField = value;
                    this.RaisePropertyChanged("code_postal");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WindowsFormsApplication1.ServiceWebAffichagePrix.Enseigne enseigne {
            get {
                return this.enseigneField;
            }
            set {
                if ((object.ReferenceEquals(this.enseigneField, value) != true)) {
                    this.enseigneField = value;
                    this.RaisePropertyChanged("enseigne");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string id_enseigne {
            get {
                return this.id_enseigneField;
            }
            set {
                if ((object.ReferenceEquals(this.id_enseigneField, value) != true)) {
                    this.id_enseigneField = value;
                    this.RaisePropertyChanged("id_enseigne");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string id_station {
            get {
                return this.id_stationField;
            }
            set {
                if ((object.ReferenceEquals(this.id_stationField, value) != true)) {
                    this.id_stationField = value;
                    this.RaisePropertyChanged("id_station");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float lattitude {
            get {
                return this.lattitudeField;
            }
            set {
                if ((this.lattitudeField.Equals(value) != true)) {
                    this.lattitudeField = value;
                    this.RaisePropertyChanged("lattitude");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float longitude {
            get {
                return this.longitudeField;
            }
            set {
                if ((this.longitudeField.Equals(value) != true)) {
                    this.longitudeField = value;
                    this.RaisePropertyChanged("longitude");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WindowsFormsApplication1.ServiceWebAffichagePrix.Prix[] price_list {
            get {
                return this.price_listField;
            }
            set {
                if ((object.ReferenceEquals(this.price_listField, value) != true)) {
                    this.price_listField = value;
                    this.RaisePropertyChanged("price_list");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Enseigne", Namespace="http://schemas.datacontract.org/2004/07/FuelTracker_Lib")]
    [System.SerializableAttribute()]
    public partial class Enseigne : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string enseigne_nameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string id_enseigneField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string enseigne_name {
            get {
                return this.enseigne_nameField;
            }
            set {
                if ((object.ReferenceEquals(this.enseigne_nameField, value) != true)) {
                    this.enseigne_nameField = value;
                    this.RaisePropertyChanged("enseigne_name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string id_enseigne {
            get {
                return this.id_enseigneField;
            }
            set {
                if ((object.ReferenceEquals(this.id_enseigneField, value) != true)) {
                    this.id_enseigneField = value;
                    this.RaisePropertyChanged("id_enseigne");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Prix", Namespace="http://schemas.datacontract.org/2004/07/FuelTracker_Lib")]
    [System.SerializableAttribute()]
    public partial class Prix : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WindowsFormsApplication1.ServiceWebAffichagePrix.Carburant_type carburant_typeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string dateMiseAjourField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string id_stationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float priceField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WindowsFormsApplication1.ServiceWebAffichagePrix.Carburant_type carburant_type {
            get {
                return this.carburant_typeField;
            }
            set {
                if ((object.ReferenceEquals(this.carburant_typeField, value) != true)) {
                    this.carburant_typeField = value;
                    this.RaisePropertyChanged("carburant_type");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string dateMiseAjour {
            get {
                return this.dateMiseAjourField;
            }
            set {
                if ((object.ReferenceEquals(this.dateMiseAjourField, value) != true)) {
                    this.dateMiseAjourField = value;
                    this.RaisePropertyChanged("dateMiseAjour");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string id_station {
            get {
                return this.id_stationField;
            }
            set {
                if ((object.ReferenceEquals(this.id_stationField, value) != true)) {
                    this.id_stationField = value;
                    this.RaisePropertyChanged("id_station");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float price {
            get {
                return this.priceField;
            }
            set {
                if ((this.priceField.Equals(value) != true)) {
                    this.priceField = value;
                    this.RaisePropertyChanged("price");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Carburant_type", Namespace="http://schemas.datacontract.org/2004/07/FuelTracker_Lib")]
    [System.SerializableAttribute()]
    public partial class Carburant_type : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string id_typeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string type_nomField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string id_type {
            get {
                return this.id_typeField;
            }
            set {
                if ((object.ReferenceEquals(this.id_typeField, value) != true)) {
                    this.id_typeField = value;
                    this.RaisePropertyChanged("id_type");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string type_nom {
            get {
                return this.type_nomField;
            }
            set {
                if ((object.ReferenceEquals(this.type_nomField, value) != true)) {
                    this.type_nomField = value;
                    this.RaisePropertyChanged("type_nom");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceWebAffichagePrix.IAffichagePrix")]
    public interface IAffichagePrix {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAffichagePrix/GetPrixCodePostal", ReplyAction="http://tempuri.org/IAffichagePrix/GetPrixCodePostalResponse")]
        WindowsFormsApplication1.ServiceWebAffichagePrix.Station[] GetPrixCodePostal(int codePostal);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAffichagePrix/GetPrixCodePostal", ReplyAction="http://tempuri.org/IAffichagePrix/GetPrixCodePostalResponse")]
        System.Threading.Tasks.Task<WindowsFormsApplication1.ServiceWebAffichagePrix.Station[]> GetPrixCodePostalAsync(int codePostal);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAffichagePrix/GetPrixDepartement", ReplyAction="http://tempuri.org/IAffichagePrix/GetPrixDepartementResponse")]
        string[] GetPrixDepartement(int departement);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAffichagePrix/GetPrixDepartement", ReplyAction="http://tempuri.org/IAffichagePrix/GetPrixDepartementResponse")]
        System.Threading.Tasks.Task<string[]> GetPrixDepartementAsync(int departement);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAffichagePrixChannel : WindowsFormsApplication1.ServiceWebAffichagePrix.IAffichagePrix, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AffichagePrixClient : System.ServiceModel.ClientBase<WindowsFormsApplication1.ServiceWebAffichagePrix.IAffichagePrix>, WindowsFormsApplication1.ServiceWebAffichagePrix.IAffichagePrix {
        
        public AffichagePrixClient() {
        }
        
        public AffichagePrixClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AffichagePrixClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AffichagePrixClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AffichagePrixClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public WindowsFormsApplication1.ServiceWebAffichagePrix.Station[] GetPrixCodePostal(int codePostal) {
            return base.Channel.GetPrixCodePostal(codePostal);
        }
        
        public System.Threading.Tasks.Task<WindowsFormsApplication1.ServiceWebAffichagePrix.Station[]> GetPrixCodePostalAsync(int codePostal) {
            return base.Channel.GetPrixCodePostalAsync(codePostal);
        }
        
        public string[] GetPrixDepartement(int departement) {
            return base.Channel.GetPrixDepartement(departement);
        }
        
        public System.Threading.Tasks.Task<string[]> GetPrixDepartementAsync(int departement) {
            return base.Channel.GetPrixDepartementAsync(departement);
        }
    }
}