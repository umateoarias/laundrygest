package com.example.laundrygest_android

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.laundrygest_android.data.CollectionItemDto

class collectionItemAdapter(val list: List<CollectionItemDto>) :
    RecyclerView.Adapter<collectionItemAdapter.ViewHolder>() {
    override fun onCreateViewHolder(
        parent: ViewGroup,
        viewType: Int
    ): ViewHolder {
        val layout = LayoutInflater.from(parent.context)
        return ViewHolder(layout.inflate(R.layout.collection_item_layout, parent, false))
    }

    override fun onBindViewHolder(
        holder: ViewHolder,
        position: Int
    ) {
        holder.namePiece.setText(list[position].pricelist.name)
        holder.qntPiece.setText(list[position].numPieces.toString())
        holder.pricePiece.setText(list[position].pricelist.unitPrice.toString()+"€")
    }

    override fun getItemCount() = list.size

    class ViewHolder(val view: View) : RecyclerView.ViewHolder(view) {
        val namePiece = view.findViewById<TextView>(R.id.piece_name)
        val qntPiece = view.findViewById<TextView>(R.id.piece_quantity)
        val pricePiece = view.findViewById<TextView>(R.id.piece_price)
    }

}