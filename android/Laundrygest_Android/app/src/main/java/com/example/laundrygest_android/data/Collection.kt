package com.example.laundrygest_android.data

import android.os.Parcel
import android.os.Parcelable
import com.google.gson.annotations.SerializedName
import java.math.BigDecimal
import java.util.Date

data class CollectionDto(
    @SerializedName("Number")
    val number: Int,
    @SerializedName("CreatedAt")
    val createdAt: Date,
    @SerializedName("DueDate")
    val dueDate: Date?,
    @SerializedName("TaxBase")
    val taxBase: BigDecimal?,
    @SerializedName("TaxAmount")
    val taxAmount: BigDecimal?,
    @SerializedName("Total")
    val total: BigDecimal?,
    @SerializedName("ClientCode")
    val clientCode: Int?,
    @SerializedName("CollectionTypeCode")
    val collectionTypeCode: Int,
    @SerializedName("InvoiceId")
    val invoiceId: Int?,
    @SerializedName("DueTotal")
    val dueTotal: BigDecimal?,
    @SerializedName("PaymentMode")
    val paymentMode: String?,
    @SerializedName("CollectionItems")
    val collectionItems: List<CollectionItemDto> = emptyList(),
    @SerializedName("CollectionTypeCodeNavigation")
    val collectionType: CollectionTypeDto
) : Parcelable {
    override fun describeContents(): Int = 0

    override fun writeToParcel(dest: Parcel, flags: Int) {
        dest.let {
            dest.writeInt(number)
            dest.writeSerializable(createdAt)
            dest.writeSerializable(dueDate)
            dest.writeSerializable(taxBase)
            dest.writeSerializable(taxAmount)
            dest.writeSerializable(total)
            dest.writeInt(clientCode!!)
            dest.writeInt(collectionTypeCode)
            dest.writeInt(invoiceId!!)
            dest.writeSerializable(dueTotal)
            dest.writeString(paymentMode)
            dest.writeList(collectionItems)
            dest.writeParcelable(collectionType,flags)
        }
    }
}

data class CollectionItemDto(
    @SerializedName("Id")
    val id: Int,
    @SerializedName("CollectionNumber")
    val collectionNumber: Int,
    @SerializedName("CollectedAt")
    val collectedAt: Date?,
    @SerializedName("NumPieces")
    val numPieces: Int,
    @SerializedName("PricelistCode")
    val pricelistCode : Int,
    @SerializedName("PricelistCodeNavigation")
    val pricelist : PricelistDTO
)

data class CollectionTypeDto(
    @SerializedName("Code")
    val code: Int,
    @SerializedName("Name")
    val description: String,
    @SerializedName("Collections")
    val collections : List<CollectionDto> = emptyList(),
    @SerializedName("Pricelists")
    val pricelists : List<PricelistDTO> = emptyList()
) : Parcelable {
    override fun describeContents(): Int =0

    override fun writeToParcel(dest: Parcel, flags: Int) {
        dest.let {
            dest.writeInt(code)
            dest.writeString(description)
            dest.writeList(collections)
            dest.writeList(pricelists)
        }
    }
}

data class PricelistDTO(
    @SerializedName("Code")
    val code : Int,
    @SerializedName("Name")
    val name : String,
    @SerializedName("UnitPrice")
    val unitPrice : BigDecimal,
    @SerializedName("CollectionTypeCode")
    val collectionTypeCode : Int,
    @SerializedName("NumPieces")
    val numPieces: Int,
    @SerializedName("CollectionTypeCodeNavigation")
    val collectionType : CollectionTypeDto
)
